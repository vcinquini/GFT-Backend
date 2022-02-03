﻿using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface IListsHandler
	{
		List<Item> CreateFinalList(int dayTime, List<Item> items);
		Task<List<Item>> FillFoodNameAsync(int dayTime, List<Item> items);
		List<Item> InputStringToList(string[] inputs);
		List<Item> OrderList(List<Item> items);
	}
}