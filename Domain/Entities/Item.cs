
namespace Domain.Entities
{
	public class Item : Base
	{
		//public int Id { get; set; }

		public int DishType { get; set; }

		public string Food { get; set; }

		public int Qty { get; set; }
	}
}