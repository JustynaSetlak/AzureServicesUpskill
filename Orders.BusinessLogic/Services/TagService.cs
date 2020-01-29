using AutoMapper;
using Orders.BusinessLogic.Dtos.Tag;
using Orders.BusinessLogic.Services.Interfaces;
using Orders.Results;
using Orders.Storage.QueueStorage.Services.Interfaces;
using Orders.TableStorage.Dtos;
using Orders.TableStorage.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Orders.Services
{
    public class TagService : ITagService
    {
        private readonly ITagTableRepository _tagRepository;
        private readonly ITagManagementQueueStorageService _tagManagementQueueStorage;
        private readonly IMapper _mapper;

        public TagService(ITagTableRepository tagRepository, ITagManagementQueueStorageService tagManagementQueueStorage, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _tagManagementQueueStorage = tagManagementQueueStorage;
            _mapper = mapper;
        }

        public async Task<DataResult<string>> InsertTag(CreateTagDto newTag)
        {
            var guidIdentificator = Guid.NewGuid().ToString();

            var tag = new TagDto
            {
                Id = guidIdentificator,
                Name = newTag.Name,
                Description = newTag.Description
            };

            var insertResult = await _tagRepository.InsertOrMerge(tag);
            var result = new DataResult<string>(insertResult.IsSuccessfull, insertResult.Value.Id);

            return result;
        }

        public async Task<bool> Delete(string id)
        {
            var tagToDelete = await _tagRepository.Get(id);

            if (!tagToDelete.IsSuccessfull)
            {
                return false;
            }

            var result = await _tagRepository.Delete(tagToDelete.Value);

            return result.IsSuccessfull;
        }

        public async Task<DataResult<DetailsTagDto>> Get(string id)
        {
            var tag = await _tagRepository.Get(id);

            var result = _mapper.Map<DataResult<DetailsTagDto>>(tag);
            return result;
        }

        public async Task<bool> InsertOrMerge(UpsertTagDto upsertTag)
        {
            var tag = _mapper.Map<TagDto>(upsertTag);
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
