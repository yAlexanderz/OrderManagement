using System.Threading.Tasks;
using OrderManagementSystem.Application.Features.Orders.Commands;

namespace OrderManagementSystem.Application.Interfaces
{
    public interface ICreateOrderCommandHandler
    {
        Task<Guid> HandleAsync(CreateOrderCommand command);
    }
}
