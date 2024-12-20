using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Application.Interfaces
{

    public interface IOrderReadRepository
    {

        Task<IEnumerable<Order>> GetAllAsync();

        Task<Order> GetByIdAsync(Guid orderId);

        Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customerId);

        Task<IEnumerable<Order>> GetByStatusAsync(string status);
    }
}
