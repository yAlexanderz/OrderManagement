using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Interfaces;

namespace OrderManagementSystem.Application.Interfaces
{

    public interface IOrderRepository : IAggregateRoot
    {
        Task AddAsync(Order order);
        Task<Order> GetByIdAsync(Guid orderId);
        Task<IEnumerable<Order>> GetAllAsync();
        Task UpdateAsync(Order order);
        Task DeleteAsync(Guid orderId);
    }
}
