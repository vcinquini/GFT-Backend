﻿using Domain;
using System.Threading.Tasks;

namespace Persistense.Interfaces
{
    public interface IFoodRepository
    {
        //Task<Food> AddAsync(Food food);
        Task<Food> GetAsync(int id);
    }
}