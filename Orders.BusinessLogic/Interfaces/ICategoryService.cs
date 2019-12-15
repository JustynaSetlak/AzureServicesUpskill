using Orders.Dtos;
using Orders.Models;
using Orders.Results;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> Delete(string rowKey);

        Task<DataResult<string>> InsertCategory(CreateCategoryDto newCategory);

        Task<bool> UpdateDescription(UpdateCategoryDto updateCategoryDto);

        Task<DataResult<Category>> Get(string id);
    }
}
