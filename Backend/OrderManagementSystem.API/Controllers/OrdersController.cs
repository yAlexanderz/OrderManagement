using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.Features.Orders.Commands;
using OrderManagementSystem.Application.Features.Orders.Handlers;
using OrderManagementSystem.Application.Interfaces;
using OrderManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderManagementSystem.API.Controllers
{
    /// <summary>
    /// Controlador para gerenciar operações relacionadas a pedidos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ICreateOrderCommandHandler _createOrderCommandHandler;
        private readonly IGetOrdersQueryHandler _getOrdersQueryHandler;
        private readonly IGetOrderByIdQueryHandler _getOrderByIdQueryHandler;

        /// <summary>
        /// Construtor do controlador Orders.
        /// </summary>
        public OrdersController(
            ICreateOrderCommandHandler createOrderCommandHandler,
            IGetOrdersQueryHandler getOrdersQueryHandler,
            IGetOrderByIdQueryHandler getOrderByIdQueryHandler)
        {
            _createOrderCommandHandler = createOrderCommandHandler;
            _getOrdersQueryHandler = getOrdersQueryHandler;
            _getOrderByIdQueryHandler = getOrderByIdQueryHandler;
        }

        /// <summary>
        /// Retorna todos os pedidos.
        /// </summary>
        /// <returns>Lista de pedidos.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _getOrdersQueryHandler.HandleAsync();
            return Ok(orders);
        }

        /// <summary>
        /// Retorna um pedido específico pelo ID.
        /// </summary>
        /// <param name="id">ID do pedido.</param>
        /// <returns>Pedido correspondente.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _getOrderByIdQueryHandler.HandleAsync(id);

            if (order == null)
            {
                return NotFound(new { Message = $"Order with ID {id} was not found." });
            }

            return Ok(order);
        }

        /// <summary>
        /// Realiza uma nova compra.
        /// </summary>
        /// <param name="command">Comando contendo os dados da compra.</param>
        /// <returns>O ID do pedido recém-criado.</returns>
        [HttpPost("PlaceOrder")]
        public async Task<ActionResult> PlaceOrder([FromBody] CreateOrderCommand command)
        {
            if (command == null || command.Items == null || command.Items.Count == 0)
            {
                return BadRequest(new { Message = "Order must contain at least one item." });
            }

            if (command.CustomerId == Guid.Empty)
            {
                return BadRequest(new { Message = "CustomerId is required." });
            }

            var orderId = await _createOrderCommandHandler.HandleAsync(command);

            return CreatedAtAction(nameof(GetOrder), new { id = orderId }, new { OrderId = orderId });
        }

    }
}
