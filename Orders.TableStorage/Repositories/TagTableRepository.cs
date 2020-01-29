using AutoMapper;
using Orders.Results;
using Orders.TableStorage.Dtos;
using Orders.TableStorage.Models;
using Orders.TableStorage.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Orders.DataAccess.TableRepositories
{
    public class TagTableRepository : ITagTableRepository
    {
        private readonly IGenericTableRepository<Tag> _tableRepository;
        private readonly IMapper _mapper;
        private readonly string tagPartitionKey = nameof(Tag);

        public TagTableRepository(IGenericTableRepository<Tag> tagRepository, IMapper mapper)
        {
            _tableRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task CreateTable()
        {
            await _tableRepository.CreateTable();
        }

        public async Task<DataResult<TagDto>> Get(string id)
        {
            var operationResult = await _tableRepository.Get(tagPartitionKey, id);
            var result = _mapper.Map<DataResult<TagDto>>(operationResult);

            return result;
        }

        public async Task<DataResult<TagDto>> InsertOrMerge(TagDto element)
        {
            var elementToInsert = _mapper.Map<Tag>(element);
            var operationResult = await _tableRepository.InsertOrMerge(elementToInsert);
            var result = _mapper.Map<DataResult<TagDto>>(operationResult);

            return result;
        }

        public async Task<DataResult<TagDto>> Delete(TagDto element)
        {
            var elementToDelete = _mapper.Map<Tag>(element);
            var operationResult = await _tableRepository.Delete(elementToDelete);
            var result = _mapper.Map<DataResult<TagDto>>(operationResult);

            return result;
        }
    }
}
