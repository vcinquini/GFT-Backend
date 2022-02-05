using Application.Interfaces;
using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Rules
{
	public class RulesHandler : IRulesHandler
	{

		public List<Item> ApplyMorningRules(List<Item> items)
		{
			List<Item> temp = new List<Item>();

			// search for the element in the list
			// if any, so this is an error, add this item as an error
			// and stops processing
			foreach (Item it in items)
			{
				if (it.DishType == (int)Constants.DishType.Dessert /*DESSERT_ID*/ || !Enum.IsDefined((Constants.DishType)it.DishType))      //!Constants.DISHTYPES.Contains(it.DishType))
				{
					temp.Add(new Item() { DishType = Constants.ERROR_ID });
					break;
				}

				// search in the final list if the item exists
				Item exists = temp.Find(x => x.DishType == it.DishType);
				
				// doesn't exist, then add it
				if (exists == null)
				{
					temp.Add(it);
				}
				else
				{
					// you can drink multiples coffees at breaktfast, otherwise it's an error
					if (exists.DishType != (int)Constants.DishType.Drink)
					{
						temp.Add(new Item() { DishType = Constants.ERROR_ID });
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

		public List<Item> ApplyNightRules(List<Item> items)
		{
			List<Item> temp = new List<Item>();

			// search for the element in the list
			// if any, so this is an error, add this item as an error
			// and stops processing
			foreach (Item it in items)
			{
				// search in the final list if the item exists
				//if (!Constants.DISHTYPES.Contains(it.DishType))
				if (!Enum.IsDefined((Constants.DishType)it.DishType))
				{
					temp.Add(new Item() { DishType = Constants.ERROR_ID });
					break;
				}

				// search in the final list if the item exists
				Item exists = temp.Find(x => x.DishType == it.DishType);

				// doesn't exist, then add it
				if (exists == null)
				{
					temp.Add(it);
				}
				else
				{
					// you can have multiples potatoes at dinner, otherwise it's an error
					if (exists.DishType != (int)Constants.DishType.Side)
					{
						temp.Add(new Item() { DishType = Constants.ERROR_ID });
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

	}
}
