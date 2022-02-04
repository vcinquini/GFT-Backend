using Domain.Entities;
using Domain.Interfaces;
using System.Threading.Tasks;

namespace Persistense.Repositories
{
	public sealed class MenuRepository : IMenuRepository
    {
        private readonly DishesContext _dbContext;

        public MenuRepository(DishesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Menu> GetAsync(int dishId, int dayTimeId) => await _dbContext.Menus.FindAsync(dishId, dayTimeId);
	}
}
