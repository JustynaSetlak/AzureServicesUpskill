using Microsoft.AspNetCore.Http;
using Orders.Common.Results;
using Orders.Dtos.Order;
using Orders.Results;
using System.Threading.Tasks;

namespace Orders.Services.Interfaces
{
    public interface IOrderService
    {
        Task<DataResult<string>> CreateOrder(CreateOrderDto createOrderDto);

        Task<OrderDto> Get(string id);

        Task<bool> UploadOrderImage(string id, string uploadedFileName, IFormFile uploadedFile);

        Task DeleteImage(string orderId);
    }
}
