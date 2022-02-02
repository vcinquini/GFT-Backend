using Application.Interfaces;
using Domain;
using Persistense.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
	public class OrderService : IOrderService
	{
		private readonly IInputValidator _inputValidator;
		private readonly IFoodRepository _foodRepository;
		private readonly IOrderRepository _orderRepository;
		private readonly IMenuRepository _menuRepository;

		public OrderService(	IInputValidator inputValidator, 
							IFoodRepository foodRepository, 
							IOrderRepository orderRepository,
							IMenuRepository menuRepository)
		{
			_inputValidator = inputValidator;
			_foodRepository = foodRepository;
			_orderRepository = orderRepository;
			_menuRepository = menuRepository;
		}

		public async Task<string> ProcessOrderAsync(string input)
		{
			int dayTime;
			string[] inputs;
			string output = "";
			List<Item> buffer;
			Order order;

			inputs = input.Split(",");

			_inputValidator.CheckInputArguments(inputs);

			dayTime = inputs[0] == Constants.MORNING ? Constants.MORNING_ID : Constants.NIGHT_ID;
			
			buffer = CreateFullList(inputs);
			buffer = CreateFinalList(dayTime, buffer);
			buffer = CreateOrderedList(buffer);

			order = await CreateOrderAsync(dayTime, buffer);

			output = ParseOrder(order);
			return output;
		}

		private List<Item> CreateFullList(string[] inputs)
		{
			int dishType;
			Item item;

			// 1st: create a list of dishes
			List<Item> items = new List<Item>();

			for (int i = 1; i < inputs.Length; i++)
			{
				if (Int32.TryParse(inputs[i], out dishType))
				{
					// add new item
					item = new Item()
					{
						DishType = dishType,
						Qty = 1,
					};
					items.Add(item);
				}
			}

			return items;
		}

		private List<Item> CreateFinalList(int dayTime, List<Item> items)
		{
			List<Item> finalList = new List<Item>();

			switch (dayTime)
			{
				case Constants.MORNING_ID:
					finalList.AddRange(ApplyMorningRules(items));
					break;
				case Constants.NIGHT_ID:
					finalList.AddRange(ApplyNightRules(items));
					break;
			}

			return finalList;
		}

		private List<Item> ApplyMorningRules(List<Item> items)
		{
			List<Item> temp = new List<Item>();

			// procura se ja existe elemento na lista 
			// se tiver, é erro, inclui item com error
			// e para o processamento
			foreach(Item it in items)
			{
				if (it.DishType == Constants.DESSERT_ID || !Constants.DISHTYPES.Contains(it.DishType))
				{
					temp.Add(new Item() { DishType = Constants.ERROR_ID, Food = Constants.ERROR, Qty = 0 });
					break;
				}

				// procura na lista final se ja existe
				Item exists = temp.Find(x => x.DishType == it.DishType);

				if(exists == null)
				{
					temp.Add(it);
				}
				else
				{
					if(exists.DishType != Constants.DRINK_ID)
					{
						temp.Add(new Item() { DishType = Constants.ERROR_ID, Food = Constants.ERROR, Qty = 0 });
						break;
					}
					else
					{
						exists.Qty++; // is a coffee
					}
				}
			}

			return temp;
		}

		private List<Item> ApplyNightRules(List<Item> items)
		{
			List<Item> temp = new List<Item>();

			// procura se ja existe elemento na lista 
			// se tiver, é erro, inclui item com error
			// e para o processamento
			foreach (Item it in items)
			{
				// verifica se é um dish type vlido
				if(!Constants.DISHTYPES.Contains(it.DishType))
				{
					temp.Add(new Item() { DishType = Constants.ERROR_ID, Food = Constants.ERROR, Qty = 0 });
					break;
				}

				// procura na lista final se ja existe
				Item exists = temp.Find(x => x.DishType == it.DishType);
				if (exists == null)
				{
					temp.Add(it);
				}
				else
				{
					if (exists.DishType != Constants.SIDE_ID)
					{
						temp.Add(new Item() { DishType = Constants.ERROR_ID, Food = Constants.ERROR, Qty = 0 });
						break;
					}
					else
					{
						exists.Qty++; 
					}
				}
			}

			return temp;
		}

		private List<Item> CreateOrderedList(List<Item> items)
		{
			return items.OrderBy(x => x.DishType).ToList();
		}

		private async Task<Order> CreateOrderAsync(int dayTime, List<Item> items)
		{
			Order order = new Order();
			Menu dl;
			Food food;

			order.Items = new List<Item>();
			order.DateOrder = DateTime.Now;

			foreach (Item it in items)
			{
				if (it.DishType != Constants.ERROR_ID)
				{
					dl = await _menuRepository.GetAsync(dayTime, it.DishType);
					food = await _foodRepository.GetAsync(dl.FoodId);
					it.Food = food.Name;
				}
				order.Items.Add(it);
			}

			order = await _orderRepository.AddAsync(order);

			return order;
		}

		private string ParseOrder(Order order)
		{
			StringBuilder output = new StringBuilder("Output: ");

			foreach (Item it in order.Items)
			{
				output.Append(it.Food);
				if(it.Qty > 1)
				{
					output.AppendFormat("(x{0})", it.Qty);
				}
				output.Append(", ");
			}

			int pos = output.ToString().LastIndexOf(',');

			return output.ToString().Remove(pos);
		}
	}
}
