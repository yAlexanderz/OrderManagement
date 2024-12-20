using System.Collections.Generic;
using System.Threading.Tasks;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Application.Interfaces
{
    public interface IGetOrdersQueryHandler
    {
        Task<IEnumerable<Order>> HandleAsync();
    }
}
