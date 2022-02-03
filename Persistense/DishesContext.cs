using Domain;
using Microsoft.EntityFrameworkCore;
using Persistense.Seeders;
using System;

namespace Persistense
{
	public class DishesContext : DbContext
	{

		public DbSet<Food> Foods { get; set; }
		public DbSet<Menu> Menus { get; set; }
		public DbSet<Order> Orders { get; set; }

		public DishesContext(DbContextOptions<DishesContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new FoodSeeder());
			modelBuilder.ApplyConfiguration(new MenuSeeder());

			base.OnModelCreating(modelBuilder);
		}
	}
}
