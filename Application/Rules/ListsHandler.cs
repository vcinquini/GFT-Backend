using Application.Interfaces;
using Domain;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Rules
{
	public class ListsHandler : IListsHandler
	{
		private readonly IFoodRepository _foodRepository;
		private readonly IMenuRepository _menuRepository;
		private readonly IRulesHandler _rulesHandler;

		public ListsHandler(IFoodRepository foodRepository,
							IMenuRepository menuRepository,
							IRulesHandler rulesHandler)
		{
			_foodRepository = foodRepository;
			_menuRepository = menuRepository;
			_rulesHandler = rulesHandler;
		}


		public List<Item> InputStringToList(OrderDTO order)
		{
			int dishType;
			Item item;

			// 1st: create a list of dishes
			List<Item> items = new List<Item>();

			foreach(var it in order.Items)
			{
				if (Int32.TryParse(it, out dishType))
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


		public List<Item> CreateFinalList(int dayTime, List<Item> items)
		{
			List<Item> finalList = new List<Item>();

			switch (dayTime)
			{
				case Constants.MORNING_ID:
					finalList.AddRange(_rulesHandler.ApplyMorningRules(items));
					break;
				case Constants.NIGHT_ID:
					finalList.AddRange(_rulesHandler.ApplyNightRules(items));
					break;
			}

			return finalList;
		}

		public List<Item> OrderList(List<Item> items)
		{
			return items.OrderBy(x => x.DishType).ToList();
		}

		public async Task<List<Item>> FillFoodNameAsync(int dayTime, List<Item> items)
		{
			Menu menu;
			Food food;
			List<Item> newList = new List<Item>();

			foreach (Item it in items)
			{
				if (it.DishType != Constants.ERROR_ID)
				{
					menu = await _menuRepository.GetAsync(dayTime, it.DishType);
					food = await _foodRepository.GetAsync(menu.FoodId);
					it.Food = food.Name;
				}
				else
				{
					it.Food = Constants.ERROR;
				}
				newList.Add(it);
			}

			return newList;
		}
	}
}
