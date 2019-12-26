using Orders.BusinessLogic.Dtos.Category;
using Orders.DataAccess.Repositories.Interfaces;
using Orders.Models;
using Orders.Results;
using Orders.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Orders.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseTableDbGenericRepository<Category> _categoryRepository;

        public CategoryService(IBaseTableDbGenericRepository<Category> documentGenericRepository)
        {
            _categoryRepository = documentGenericRepository;
        }

        public async Task<DataResult<string>> InsertCategory(CreateCategoryDto newCategory)
        {
            var guidIdentificator = Guid.NewGuid().ToString();

            var category = new Category
            {
                RowKey = guidIdentificator,
                PartitionKey = nameof(Category),
                Name = newCategory.Name,
                Description = newCategory.Description
            };

            var insertResult = await _categoryRepository.Insert(category);
            var result = new DataResult<string>(insertResult.IsSuccessfull, insertResult.Value.RowKey);

            return result;
        }

        public async Task<bool> UpdateDescription(UpdateCategoryDto updateCategoryDto)
        {
            var getResult = await Get(updateCategoryDto.Id);

            if (!getResult.IsSuccessfull)
            {
                return false;
            }

            getResult.Value.Description = updateCategoryDto.Description;

            var result = await _categoryRepository.Replace(getResult.Value);

            return result.IsSuccessfull;
        }

        public async Task<bool> Delete(string id)
        {
            var getResult = await Get(id);

            if (!getResult.IsSuccessfull)
            {
                return false;
            }

            var result = await _categoryRepository.Delete(getResult.Value);

            return result.IsSuccessfull;
        }

        public async Task<DataResult<Category>> Get(string id)
        {
            var category = await _categoryRepository.Get(nameof(Category), id);

            return category;
        }
    }
}
