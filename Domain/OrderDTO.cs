using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
	public class OrderDTO
	{
		public string DayTime { get; set; }
		public List<string> Items { get; set; }
	}
}
