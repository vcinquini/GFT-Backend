using Domain.Entities;
using Domain.Interfaces;
using System.Threading.Tasks;

namespace Persistense.Repositories
{
	public sealed class FoodRepository : IFoodRepository
    {
        private readonly DishesContext _dbContext;

        public FoodRepository(DishesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Food> GetAsync(int id) => await _dbContext.Foods.FindAsync(id);
    }
}
