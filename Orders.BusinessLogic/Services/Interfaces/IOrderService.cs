using Microsoft.AspNetCore.Http;
using Orders.BusinessLogic.Dtos.Order;
using Orders.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.BusinessLogic.Services.Interfaces
{
    public interface IOrderService : IService
    {
        Task<DataResult<string>> CreateOrder(CreateOrderDto createOrderDto);

        Task<OrderDetailsDto> Get(string id);

        Task<bool> AssignOrderImage(string id, string uploadedFileName, IFormFile uploadedFile);

        Task DeleteImage(string orderId);

        Task<List<OrderDetailsDto>> Search(SearchOrderParamsDto searchOrderParams);

        Task UpdatePrice(string id, double price);
    }
}
