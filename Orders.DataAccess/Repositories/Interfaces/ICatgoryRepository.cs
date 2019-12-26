using Orders.Models;
using Orders.Results;
using System.Threading.Tasks;

namespace Orders.DataAccess.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<DataResult<Category>> Get(string id);
    }
}
