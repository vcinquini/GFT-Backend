﻿using Domain;
using System.Threading.Tasks;

namespace Persistense.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> AddAsync(Order order);
        Task<Order> GetAsync(int id);
    }
}