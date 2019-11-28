using Microsoft.AspNetCore.Mvc;
using Orders.Models;
using Orders.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpPost()]
        public async Task<ActionResult> Create([FromBody] Category categoryToAdd)
        {
            var result = await _categoryService.InsertCategory(categoryToAdd);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
