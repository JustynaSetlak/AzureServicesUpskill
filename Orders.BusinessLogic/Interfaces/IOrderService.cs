using Microsoft.AspNetCore.Http;
using Orders.BusinessLogic.Dtos.Order;
using Orders.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.Services.Interfaces
{
    public interface IOrderService
    {
        Task<DataResult<string>> CreateOrder(CreateOrderDto createOrderDto);

        Task<OrderDto> Get(string id);

        Task<bool> UploadOrderImage(string id, string uploadedFileName, IFormFile uploadedFile);

        Task DeleteImage(string orderId);

        Task<List<OrderDto>> Search(SearchOrderParamsDto searchOrderParams);
    }
}
