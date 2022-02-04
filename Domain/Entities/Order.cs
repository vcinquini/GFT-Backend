using System;
using System.Collections.Generic;

namespace Domain.Entities
{
	public class Order : Base
	{
		//public int Id { get; set; }
		public List<Item> Items { get; set; }
		public DateTime DateOrder { get; set; }

	}
}
