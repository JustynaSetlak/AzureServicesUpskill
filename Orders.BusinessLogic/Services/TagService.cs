using AutoMapper;
using Orders.BusinessLogic.Dtos.Tag;
using Orders.DataAccess.Repositories.Interfaces;
using Orders.DataAccess.Storage.Interfaces;
using Orders.DataAccess.TableRepositories.Interfaces;
using Orders.DataAccess.TableRepositories.Models;
using Orders.Results;
using Orders.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Orders.Services
{
    public class TagService : ITagService
    {
        private readonly ITagTableRepository _tagRepository;
        private readonly ITagManagementQueueStorage _tagManagementQueueStorage;
        private readonly IMapper _mapper;

        public TagService(ITagTableRepository tagRepository, ITagManagementQueueStorage tagManagementQueueStorage, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _tagManagementQueueStorage = tagManagementQueueStorage;
            _mapper = mapper;
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

            var insertResult = await _tagRepository.InsertOrMerge(tag);
            var result = new DataResult<string>(insertResult.IsSuccessfull, insertResult.Value.RowKey);

            return result;
        }

        public async Task<bool> Delete(string id)
        {
            var getResult = await Get(id);

            if (!getResult.IsSuccessfull)
            {
                return false;
            }

            var result = await _tagRepository.Delete(getResult.Value);

            return result.IsSuccessfull;
        }

        public async Task<DataResult<Tag>> Get(string id)
        {
            var tag = await _tagRepository.Get(id);

            return tag;
        }

        public async Task<bool> InsertOrMerge(UpsertTagDto upsertTag)
        {
            var tag = _mapper.Map<Tag>(upsertTag);
            var result = await _tagRepository.InsertOrMerge(tag);

            return result.IsSuccessfull;
        }

        public async Task<Task<bool>> HandleMarketingTagModificationRequest()
        {
            var tagToUpsert = await _tagManagementQueueStorage.RetrieveMessage();

            var tag = _mapper.Map<UpsertTagDto>(tagToUpsert);
            var tagUpsertResult = InsertOrMerge(tag);

            return tagUpsertResult;
        }
    }
}
