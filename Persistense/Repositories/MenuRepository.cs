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
    public sealed class MenuRepository : IMenuRepository
    {
        private readonly DishesContext _dbContext;

        public MenuRepository(DishesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Menu> GetAsync(int dishId, int dayTimeId) => await _dbContext.Menus.FindAsync(dishId, dayTimeId);

        public async Task<Menu> AddAsync(Menu menu)
        {
            var newmenu = await _dbContext.AddAsync(menu);
            await _dbContext.SaveChangesAsync();

            return newmenu.Entity;
        }

        //public async Task UpdateAsync(Menu menu)
        //{
        //    _dbContext.Entry(menu).State = EntityState.Modified;
        //    await _dbContext.SaveChangesAsync();
        //}

        //public async Task DeleteAsync(Menu menu)
        //{
        //    _dbContext.Entry(menu).State = EntityState.Deleted;
        //    await _dbContext.SaveChangesAsync();
        //}

	}
}
