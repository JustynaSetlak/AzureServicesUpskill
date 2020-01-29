using Orders.DocumentDataAccess.Dtos;
using Orders.Results;
using System.Threading.Tasks;

namespace Orders.DataAccess.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<DataResult<string>> Create(OrderDto orderDto);

        Task<OrderDto> Get(string orderId);

        Task Replace(OrderDto order);
    }
}
