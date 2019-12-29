using Orders.BusinessLogic.Dtos.Category;
using Orders.BusinessLogic.Interfaces;
using Orders.Models;
using Orders.Results;
using System.Threading.Tasks;

namespace Orders.Services.Interfaces
{
    public interface ICategoryService : IService
    {
        Task<bool> Delete(string rowKey);

        Task<DataResult<string>> InsertCategory(CreateCategoryDto newCategory);

        Task<bool> UpdateDescription(UpdateCategoryDto updateCategoryDto);

        Task<DataResult<Category>> Get(string id);
    }
}
