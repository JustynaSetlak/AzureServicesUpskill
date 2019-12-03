using Orders.Dtos.Order;
using System.Threading.Tasks;

namespace Orders.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(CreateOrderDto createOrderDto);
    }
}
