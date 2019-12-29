using Orders.Models;
using Orders.Results;
using System.Threading.Tasks;

namespace Orders.DataAccess.TableRepositories.Interfaces
{
    public interface ITagTableRepository
    {
        Task<DataResult<Tag>> Get(string id);
    }
}
