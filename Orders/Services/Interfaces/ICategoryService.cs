using Orders.Dtos;
using Orders.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> Delete(string partitionKey, string rowKey);

        Task<bool> InsertCategory(CreateCategoryDto newCategory);

        Task<bool> UpdateDescription(UpdateCategoryDto updateCategoryDto);
    }
}
