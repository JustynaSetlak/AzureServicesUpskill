using Microsoft.AspNetCore.Http;
using Orders.Dtos.Order;
using System.Threading.Tasks;

namespace Orders.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(CreateOrderDto createOrderDto);

        Task<OrderDto> Get(string id);

        Task<bool> UploadOrderImage(string id, string uploadedFileName, IFormFile uploadedFile);

        Task DeleteImage(string orderId);
    }
}
