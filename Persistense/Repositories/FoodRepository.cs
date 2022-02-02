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
    public sealed class FoodRepository : IFoodRepository
    {
        private readonly DishesContext _dbContext;

        public FoodRepository(DishesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Food> GetAsync(int id) => await _dbContext.Foods.FindAsync(id);


        public async Task<Food> AddAsync(Food food)
        {
            var newFood = await _dbContext.Foods.AddAsync(food);
            await _dbContext.SaveChangesAsync();

            return newFood.Entity;
        }

        //public async Task UpdateAsync(Food food)
        //{
        //    _dbContext.Entry(food).State = EntityState.Modified;
        //    await _dbContext.SaveChangesAsync();
        //}

        //public async Task DeleteAsync(Food food)
        //{
        //    _dbContext.Entry(food).State = EntityState.Deleted;
        //    await _dbContext.SaveChangesAsync();
        //}

    }
}
