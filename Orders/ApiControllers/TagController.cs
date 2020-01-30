using Microsoft.AspNetCore.Mvc;
using Orders.BusinessLogic.Dtos.Tag;
using Orders.BusinessLogic.Services.Interfaces;
using System.Threading.Tasks;

namespace Orders.Api.ApiControllers
{
    [Route("/api/tag")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateTagDto tagToAdd)
        {
            var result = await _tagService.InsertTag(tagToAdd);

            if (!result.IsSuccessfull)
            {
                return BadRequest();
            }

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var result = await _tagService.Get(id);

            if (result.IsSuccessfull)
            {
                return BadRequest();
            }

            return Ok(result.Value);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpsertTagDto tag)
        {
            var result = await _tagService.InsertOrMerge(tag);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _tagService.Delete(id);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
