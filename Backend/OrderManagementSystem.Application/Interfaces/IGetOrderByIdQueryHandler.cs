using System;
using System.Threading.Tasks;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Application.Interfaces
{
    public interface IGetOrderByIdQueryHandler
    {
        Task<Order> HandleAsync(Guid orderId);
    }
}
