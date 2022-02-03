using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistense
{
	public interface IDishesContext
	{
		DbSet<Food> Foods { get; set; }
		DbSet<Menu> Menus { get; set; }
		DbSet<Order> Orders { get; set; }
	}
}