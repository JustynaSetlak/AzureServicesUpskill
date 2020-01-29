using AutoMapper;
using Orders.BusinessLogic.Dtos.Category;
using Orders.BusinessLogic.Services.Interfaces;
using Orders.Results;
using Orders.TableStorage.Dtos;
using Orders.TableStorage.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Orders.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryTableRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryTableRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<DataResult<string>> InsertCategory(CreateCategoryDto newCategory)
        {
            var guidIdentificator = Guid.NewGuid().ToString();

            var category = new CategoryDto
            {
                Id = guidIdentificator,
                Name = newCategory.Name,
                Description = newCategory.Description
            };

            var insertResult = await _categoryRepository.InsertOrMerge(category);
            var result = new DataResult<string>(insertResult.IsSuccessfull, insertResult.Value.Id);

            return result;
        }

        public async Task<bool> UpdateDescription(UpdateCategoryDto updateCategoryDto)
        {
            var getResult = await _categoryRepository.Get(updateCategoryDto.Id);

            if (!getResult.IsSuccessfull)
            {
                return false;
            }

            getResult.Value.Description = updateCategoryDto.Description;

            var result = await _categoryRepository.InsertOrMerge(getResult.Value);

            return result.IsSuccessfull;
        }

        public async Task<bool> Delete(string id)
        {
            var categoryToDelete = await _categoryRepository.Get(id);

            if (!categoryToDelete.IsSuccessfull)
            {
                return false;
            }

            var result = await _categoryRepository.Delete(categoryToDelete.Value);

            return result.IsSuccessfull;
        }

        public async Task<DataResult<DetailsCategoryDto>> Get(string id)
        {
            var category = await _categoryRepository.Get(id);

            var result = _mapper.Map<DataResult<DetailsCategoryDto>>(category);
            return result;
        }
    }
}
