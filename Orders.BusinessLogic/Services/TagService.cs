using Orders.Dtos.Tag;
using Orders.Models;
using Orders.Repositories.Interfaces;
using Orders.Results;
using Orders.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Orders.Services
{
    public class TagService : ITagService
    {
        private readonly IBaseTableDbGenericRepository<Tag> _tagRepository;
        private readonly IOrderRepository _orderRepository;

        public TagService(IBaseTableDbGenericRepository<Tag> tagRepository, IOrderRepository orderRepository)
        {
            _tagRepository = tagRepository;
            _orderRepository = orderRepository;
        }

        public async Task<DataResult<string>> InsertTag(CreateTagDto newTag)
        {
            var guidIdentificator = Guid.NewGuid().ToString();

            var tag = new Tag
            {
                RowKey = guidIdentificator,
                PartitionKey = nameof(Tag),
                Name = newTag.Name,
                Description = newTag.Description
            };

            var insertResult = await _tagRepository.Insert(tag);
            var result = new DataResult<string>(insertResult.IsSuccessfull, insertResult.Value.RowKey);

            return result;
        }

        public async Task<bool> UpdateDescription(UpdateTagDto updateTagDto)
        {
            var getResult = await Get(updateTagDto.Id);

            if (getResult.IsSuccessfull)
            {
                return false;
            }

            getResult.Value.Description = updateTagDto.Description;

            var result = await _tagRepository.Replace(getResult.Value);

            return result.IsSuccessfull;
        }

        public async Task<bool> Delete(string id)
        {
            var getResult = await Get(id);

            if (!getResult.IsSuccessfull)
            {
                return false;
            }

            //to check if orders exist

            var result = await _tagRepository.Delete(getResult.Value);

            return result.IsSuccessfull;
        }

        public async Task<DataResult<Tag>> Get(string id)
        {
            var tag = await _tagRepository.Get(nameof(Tag), id);

            return tag;
        }
    }
}
