using Domain.Entities;
using System.Collections.Generic;

namespace Application.Interfaces
{
	public interface IRulesHandler
	{
		List<Item> ApplyMorningRules(List<Item> items);
		List<Item> ApplyNightRules(List<Item> items);
	}
}