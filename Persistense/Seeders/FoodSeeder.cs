using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistense.Seeders
{
	internal class FoodSeeder : IEntityTypeConfiguration<Food>
	{
		public void Configure(EntityTypeBuilder<Food> builder)
		{
			builder.ToTable("Food");
			builder.HasKey(x => x.Id);

			builder.HasData(new Food { Id = 1, Name = "Eggs" } );
			builder.HasData(new Food { Id = 2, Name = "Toast" } );
			builder.HasData(new Food { Id = 3, Name = "Coffe" } );
			builder.HasData(new Food { Id = 4, Name = "Steak" } );
			builder.HasData(new Food { Id = 5, Name = "Potato" } );
			builder.HasData(new Food { Id = 6, Name = "Wine" } );
			builder.HasData(new Food { Id = 7, Name = "Cake" } );
		}
	}
}
