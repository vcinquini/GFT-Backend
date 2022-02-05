using System;
using System.Collections.Generic;

namespace Domain.Entities
{
	public class Order : Base
	{
		public List<Item> Items { get; set; }
		public DateTime DateOrder { get; set; }

	}
}
