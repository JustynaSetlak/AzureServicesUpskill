using Microsoft.AspNetCore.Mvc;
using Orders.Dtos;
using Orders.Models;
using Orders.Services.Interfaces;
using System.Threading.Tasks;

namespace Orders.ApiControllers
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

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
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

        [HttpDelete]
        public async Task<ActionResult> Delete(string partitionKey, string rowKey)
        {
            var result = await _categoryService.Delete(partitionKey, rowKey);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
