using AutoMapper;
using Orders.Results;
using Orders.TableStorage.Dtos;
using Orders.TableStorage.Models;
using Orders.TableStorage.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Orders.TableStorage.Repositories
{
    public class CategoryTableRepository : ICategoryTableRepository
    {
        private readonly IGenericTableRepository<Category> _tableRepository;
        private readonly IMapper _mapper;
        private readonly string categorPartitionKey = nameof(Category);

        public CategoryTableRepository(IGenericTableRepository<Category> categoryRepository, IMapper mapper)
        {
            _tableRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task CreateTable()
        {
            await _tableRepository.CreateTable();
        }

        public async Task<DataResult<CategoryDto>> Get(string id)
        {
            var operationResult = await _tableRepository.Get(categorPartitionKey, id);
            var result = _mapper.Map<DataResult<CategoryDto>>(operationResult);

            return result;
        }

        public async Task<DataResult<TagDto>> InsertOrMerge(CategoryDto categoryDto)
        {
            var categoryToInsert = _mapper.Map<Category>(categoryDto);

            var operationResult = await _tableRepository.InsertOrMerge(categoryToInsert);
            var result = _mapper.Map<DataResult<TagDto>>(operationResult);
            return result;
        }

        public async Task<DataResult<CategoryDto>> Delete(CategoryDto element)
        {
            var elementToDelete = _mapper.Map<Category>(element);
            var operationResult = await _tableRepository.Delete(elementToDelete);
            var result = _mapper.Map<DataResult<CategoryDto>>(operationResult);

            return result;
        }
    }
}
