using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orders.Dtos.Order;
using Orders.Services.Interfaces;

namespace Orders.ApiControllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public async Task<OrderDto> Get(string id)
        {
            var result = await _orderService.Get(id);

            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateOrderDto createOrderDto)
        {
            await _orderService.CreateOrder(createOrderDto);

            return Accepted();
        }
    }
}
