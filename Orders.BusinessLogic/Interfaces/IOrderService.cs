using Microsoft.AspNetCore.Http;
using Orders.BusinessLogic.Dtos.Order;
using Orders.BusinessLogic.Interfaces;
using Orders.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.Services.Interfaces
{
    public interface IOrderService : IService
    {
        Task<DataResult<string>> CreateOrder(CreateOrderDto createOrderDto);

        Task<OrderDto> Get(string id);

        Task<bool> AssignOrderImage(string id, string uploadedFileName, IFormFile uploadedFile);

        Task DeleteImage(string orderId);

        Task<List<OrderDto>> Search(SearchOrderParamsDto searchOrderParams);
    }
}
