using Orders.Results;
using Orders.TableStorage.Dtos;
using System.Threading.Tasks;

namespace Orders.TableStorage.Repositories.Interfaces
{
    public interface ITagTableRepository
    {
        Task CreateTable();

        Task<DataResult<TagDto>> Get(string id);

        Task<DataResult<TagDto>> InsertOrMerge(TagDto element);

        Task<DataResult<TagDto>> Delete(TagDto element);
    }
}
