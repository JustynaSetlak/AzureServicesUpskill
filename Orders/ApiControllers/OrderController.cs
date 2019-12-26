using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orders.BusinessLogic.Dtos.Order;
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

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchOrderParamsDto searchOrderParams)
        {
            var result = await _orderService.Search(searchOrderParams);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var result = await _orderService.Get(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateOrderDto createOrderDto)
        {
            var result = await _orderService.CreateOrder(createOrderDto);

            if (!result.IsSuccessfull)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Get), new { id = result.Value }, createOrderDto);
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
