using Microsoft.AspNetCore.Mvc;
using Orders.BusinessLogic.Dtos.Category;
using Orders.BusinessLogic.Services.Interfaces;
using System.Threading.Tasks;

namespace Orders.Api.ApiControllers
{
    [Route("/api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCategoryDto categoryToAdd)
        {
            var result = await _categoryService.InsertCategory(categoryToAdd);

            if (!result.IsSuccessfull)
            {
                return BadRequest();
            }

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var result = await _categoryService.Get(id);

            if (result.IsSuccessfull)
            {
                return BadRequest();
            }

            return Ok(result.Value);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateCategoryDto category)
        {
            var result = await _categoryService.UpdateDescription(category);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _categoryService.Delete(id);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
