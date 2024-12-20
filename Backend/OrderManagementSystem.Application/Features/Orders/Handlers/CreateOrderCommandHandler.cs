using OrderManagementSystem.Application.Features.Orders.Commands;
using OrderManagementSystem.Application.Interfaces;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementSystem.Application.Features.Orders.Handlers
{
    public class CreateOrderCommandHandler : ICreateOrderCommandHandler
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Guid> HandleAsync(CreateOrderCommand command)
        {
            if (command == null || !command.Items.Any())
            {
                throw new ArgumentException("The order must contain at least one item.");
            }

            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = command.CustomerId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                Items = command.Items.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };

            await _orderRepository.AddAsync(order);

            return order.Id;
        }
    }
}
