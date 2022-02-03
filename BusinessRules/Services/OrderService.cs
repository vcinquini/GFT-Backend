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
		private readonly IOrderRepository _orderRepository;
		private readonly IListsHandler _listsHandler;

		public OrderService(IInputValidator inputValidator, 
							IOrderRepository orderRepository,
							IListsHandler listsHandler)
		{
			_inputValidator = inputValidator;
			_orderRepository = orderRepository;
			_listsHandler = listsHandler;
		}

		public async Task<string> ProcessOrderAsync(string input)
		{
			int dayTime;
			string[] inputs;
			string output = "";
			List<Item> buffer;
			Order order;

			try
			{
				inputs = input.Split(",");

				_inputValidator.CheckInputArguments(inputs);

				dayTime = inputs[0] == Constants.MORNING ? Constants.MORNING_ID : Constants.NIGHT_ID;

				buffer = _listsHandler.InputStringToList(inputs);
				buffer = _listsHandler.CreateFinalList(dayTime, buffer);
				buffer = _listsHandler.OrderList(buffer);
				buffer = await _listsHandler.FillFoodNameAsync(dayTime, buffer);
				order = await CreateOrderAsync(buffer);

				output = ParseOrder(order);
			}
			catch (Exception ex)
			{
				output = ex.Message;
			}
			return output;
		}

		private async Task<Order> CreateOrderAsync(List<Item> items)
		{
			Order order = new Order();

			order.DateOrder = DateTime.Now;
			order.Items = items;

			var newOrder = await _orderRepository.AddAsync(order);

			return newOrder;
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

			return output.ToString().TrimEnd(new char[] { ',', ' ' });
		}
	}
}
