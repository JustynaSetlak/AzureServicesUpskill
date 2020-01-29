using Orders.BusinessLogic.Dtos.Category;
using Orders.Results;
using System.Threading.Tasks;

namespace Orders.BusinessLogic.Services.Interfaces
{
    public interface ICategoryService : IService
    {
        Task<bool> Delete(string rowKey);

        Task<DataResult<string>> InsertCategory(CreateCategoryDto newCategory);

        Task<bool> UpdateDescription(UpdateCategoryDto updateCategoryDto);

        Task<DataResult<DetailsCategoryDto>> Get(string id);
    }
}
