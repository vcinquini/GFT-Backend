using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistense.Seeders
{
	internal class MenuSeeder : IEntityTypeConfiguration<Menu>
	{
		public void Configure(EntityTypeBuilder<Menu> builder)
		{
			builder.ToTable("Menu");
			builder.HasKey(x => new { x.DayTimeId, x.DishTypeId });

			builder.HasData(new Menu { DayTimeId = 1, DishTypeId = 1, FoodId = 1 } );
			builder.HasData(new Menu { DayTimeId = 1, DishTypeId = 2, FoodId = 2 } );
			builder.HasData(new Menu { DayTimeId = 1, DishTypeId = 3, FoodId = 3 } );
			builder.HasData(new Menu { DayTimeId = 2, DishTypeId = 1, FoodId = 4 } );
			builder.HasData(new Menu { DayTimeId = 2, DishTypeId = 2, FoodId = 5 } );
			builder.HasData(new Menu { DayTimeId = 2, DishTypeId = 4, FoodId = 7 } );
			builder.HasData(new Menu { DayTimeId = 2, DishTypeId = 3, FoodId = 6 } );
		}
	}
}
