using Orders.DataAccess.TableRepositories.Models;
using Orders.Results;
using System.Threading.Tasks;

namespace Orders.DataAccess.TableRepositories.Interfaces
{
    public interface ITagTableRepository
    {
        Task<DataResult<Tag>> Get(string id);

        Task<DataResult<Tag>> InsertOrMerge(Tag element);

        Task<DataResult<Tag>> Delete(Tag element);
    }
}
