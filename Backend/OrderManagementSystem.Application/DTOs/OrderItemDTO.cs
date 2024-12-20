using System;

namespace OrderManagementSystem.Application.DTOs
{
    public class OrderItemDTO
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Calculado dinamicamente para garantir precisão
        public decimal TotalPrice => Quantity * UnitPrice;

        // Validação para garantir consistência dos dados
        public bool IsValid()
        {
            return ProductId != Guid.Empty
                && !string.IsNullOrWhiteSpace(ProductName)
                && Quantity > 0
                && UnitPrice >= 0;
        }
    }
}
