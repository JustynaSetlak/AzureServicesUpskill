using Orders.Models;
using Orders.Results;
using System.Threading.Tasks;

namespace Orders.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<DataResult<string>> CreateOrder(Order order);

        Task<Order> GetOrder(string orderId);

        Task ReplaceDocument(Order order);
    }
}
