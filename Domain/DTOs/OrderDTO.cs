using System.Collections.Generic;

namespace Domain.DTOs
{
	public class OrderDTO
	{
		public string DayTime { get; set; }
		public List<string> Items { get; set; }
	}
}
