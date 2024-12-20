using MediatR;
using OrderManagementSystem.Application.DTOs;
using System;
using System.Collections.Generic;

namespace OrderManagementSystem.Application.Features.Orders.Commands
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid CustomerId { get; set; }
        public List<OrderItemDTO> Items { get; set; }

        public CreateOrderCommand()
        {
            Items = new List<OrderItemDTO>();
        }
    }
}
