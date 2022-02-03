using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Tests
{
	static class Helper
	{
		public static Dictionary<int, Food> FoodList = new Dictionary<int, Food>()
		{ 
			{ 1, new Food(){ Id = 1, Name = "Eggs"} },
			{ 2, new Food(){ Id = 2, Name = "Toast"} },
			{ 3, new Food(){ Id = 3, Name = "Coffe" } },
			{ 4, new Food(){ Id = 4, Name = "Steak" } },
			{ 5, new Food(){ Id = 5, Name = "Potato" } },
			{ 6, new Food(){ Id = 6, Name = "Wine" } },
			{ 7, new Food(){ Id = 7, Name = "Cake" } }
		};

		public static Task<Food> GetFood(int id) => Task.FromResult(Helper.FoodList[id]);

		public static Task<Food> AddFood(Food item) 
		{
			FoodList.Add(item.Id, new Food() { Id = item.Id, Name = item.Name });
			return Task.FromResult(Helper.FoodList[item.Id]);
		}

	}
}
