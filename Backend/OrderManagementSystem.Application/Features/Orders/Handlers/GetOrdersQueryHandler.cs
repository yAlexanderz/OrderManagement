using System.Collections.Generic;
using System.Threading.Tasks;
using OrderManagementSystem.Application.Interfaces;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Application.Features.Orders.Handlers
{
    public class GetOrdersQueryHandler : IGetOrdersQueryHandler
    {
        private readonly IOrderReadRepository _orderReadRepository;

        public GetOrdersQueryHandler(IOrderReadRepository orderReadRepository)
        {
            _orderReadRepository = orderReadRepository;
        }

        public async Task<IEnumerable<Order>> HandleAsync()
        {
            return await _orderReadRepository.GetAllAsync();
        }
    }
}
