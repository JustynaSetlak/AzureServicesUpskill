using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;
using Orders.Config;
using Orders.Dtos;
using Orders.Models;
using Orders.Repositories.Interfaces;
using Orders.Services.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Orders.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CloudTable _categoryTable;
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryService(IGenericRepository<Category> genericRepository)
        {
            _categoryRepository = genericRepository;
        }

        public async Task<bool> InsertCategory(CreateCategoryDto newCategory)
        {
            var newGuid = Guid.NewGuid().ToString();

            var category = new Category
            {
                RowKey = newGuid,
                PartitionKey = newGuid,
                Name = newCategory.Name,
                Description = newCategory.Description
            };

            var result = await _categoryRepository.Insert(category);
            return result.HttpStatusCode == (int)HttpStatusCode.NoContent;
        }

        public async Task<bool> UpdateDescription(UpdateCategoryDto updateCategoryDto)
        {
            var categoryToUpdate = await _categoryRepository.Get(updateCategoryDto.PartitionKey, updateCategoryDto.RowKey);

            categoryToUpdate.Description = updateCategoryDto.Description;

            var result = await _categoryRepository.Replace(categoryToUpdate);

            return result.HttpStatusCode == (int)HttpStatusCode.NoContent;
        }

        public async Task<bool> Delete(string partitionKey, string rowKey)
        {
            var categoryToDelete = await _categoryRepository.Get(partitionKey, rowKey);
            var result = await _categoryRepository.Delete(categoryToDelete);

            return result.HttpStatusCode == (int)HttpStatusCode.NoContent;
        }
    }
}
