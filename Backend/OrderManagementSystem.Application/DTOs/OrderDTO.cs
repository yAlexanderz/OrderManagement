using System;
using System.Collections.Generic;

namespace OrderManagementSystem.Application.DTOs
{
    /// <summary>
    /// DTO que representa um pedido.
    /// </summary>
    public class OrderDTO
    {
        /// <summary>
        /// Identificador único do pedido.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Identificador do cliente associado ao pedido.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Data de criação do pedido.
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Lista de itens do pedido.
        /// </summary>
        public List<OrderItemDTO> Items { get; set; }

        /// <summary>
        /// Valor total do pedido.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Status do pedido.
        /// </summary>
        public string Status { get; set; }
    }
}
