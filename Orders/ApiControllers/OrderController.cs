using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        [HttpPost("upload/{id}")]
        public async Task<ActionResult> Post(string id, IFormFile uploadedFile)
        {
            var isSuccessful = await _orderService.UploadOrderImage(id, uploadedFile.FileName, uploadedFile);

            if (!isSuccessful)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("removeImage/{orderId}")]
        public async Task<ActionResult> DeleteImageUri(string orderId)
        {
            await _orderService.DeleteImage(orderId);

            return NoContent();
        }
    }
}
