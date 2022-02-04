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

			// procura se ja existe elemento na lista 
			// se tiver, é erro, inclui item com error
			// e para o processamento
			foreach (Item it in items)
			{
				if (it.DishType == Constants.DESSERT_ID || !Constants.DISHTYPES.Contains(it.DishType))
				{
					temp.Add(new Item() { DishType = Constants.ERROR_ID });
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
					if (exists.DishType != Constants.DRINK_ID)
					{
						temp.Add(new Item() { DishType = Constants.ERROR_ID });
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

		public List<Item> ApplyNightRules(List<Item> items)
		{
			List<Item> temp = new List<Item>();

			// procura se ja existe elemento na lista 
			// se tiver, é erro, inclui item com error
			// e para o processamento
			foreach (Item it in items)
			{
				// verifica se é um dish type vlido
				if (!Constants.DISHTYPES.Contains(it.DishType))
				{
					temp.Add(new Item() { DishType = Constants.ERROR_ID });
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
