using Orders.Models;
using Orders.Results;
using System.Threading.Tasks;

namespace Orders.DataAccess.Repositories.Interfaces
{
    public interface ITagRepository
    {
        Task<DataResult<Tag>> Get(string id);
    }
}
