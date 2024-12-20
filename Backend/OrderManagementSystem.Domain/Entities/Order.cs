using OrderManagementSystem.Domain.Enums;
using OrderManagementSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Domain.Entities
{
    public class Order : IAggregateRoot
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public List<OrderItem> Items { get; set; } = new();

        public decimal TotalAmount => Items?.Sum(i => i.TotalPrice) ?? 0;

        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
    }
}
