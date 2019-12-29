using Orders.Models;
using Orders.Results;
using System.Threading.Tasks;

namespace Orders.DataAccess.TableRepositories.Interfaces
{
    public interface ICategoryTableRepository
    {
        Task<DataResult<Category>> Get(string id);
    }
}
