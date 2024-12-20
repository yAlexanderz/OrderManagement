using FluentValidation;
using OrderManagementSystem.Application.Features.Orders.Commands;

namespace OrderManagementSystem.Application.Features.Orders.Validators
{
    /// <summary>
    /// Validador para o comando de criação de pedidos.
    /// </summary>
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            // Valida que o pedido tenha pelo menos um item
            RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage("The order must contain at least one item.")
                .Must(items => items.All(item => item.IsValid()))
                .WithMessage("One or more items in the order are invalid.");

            // Valida que cada item no pedido tenha quantidade positiva
            RuleForEach(x => x.Items)
                .ChildRules(item =>
                {
                    item.RuleFor(i => i.Quantity)
                        .GreaterThan(0)
                        .WithMessage("Each item must have a quantity greater than zero.");

                    item.RuleFor(i => i.UnitPrice)
                        .GreaterThan(0)
                        .WithMessage("Each item must have a unit price greater than zero.");
                });

            // Valida o total do pedido (caso necessário)
            RuleFor(x => x.Items.Sum(i => i.TotalPrice))
                .GreaterThan(0)
                .WithMessage("The total order amount must be greater than zero.");
        }
    }
}
