using Domain.Entities;
using Domain.Interfaces;
using System.Threading.Tasks;

namespace Persistense.Repositories
{
	public sealed class OrderRepository : IOrderRepository
    {
        private readonly DishesContext _dbContext;

        public OrderRepository(DishesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> AddAsync(Order order)
        {
            var newOrder = await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return newOrder.Entity;
        }
    }
}
