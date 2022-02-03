using Domain;
using Microsoft.EntityFrameworkCore;
using Persistense.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

       // public async Task<Order> GetAsync(int id) => await _dbContext.Orders.FindAsync(id);


        public async Task<Order> AddAsync(Order order)
        {
            var newOrder = await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return newOrder.Entity;
        }
    }
}
