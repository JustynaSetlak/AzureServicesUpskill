using Orders.Results;
using Orders.TableStorage.Dtos;
using Orders.TableStorage.Models;
using System.Threading.Tasks;

namespace Orders.TableStorage.Repositories.Interfaces
{
    public interface ICategoryTableRepository
    {
        Task CreateTable();

        Task<DataResult<CategoryDto>> Get(string id);

        Task<DataResult<TagDto>> InsertOrMerge(CategoryDto categoryDto);

        Task<DataResult<CategoryDto>> Delete(CategoryDto element);
    }
}
