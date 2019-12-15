using Orders.Models;
using Orders.Results;
using System.Threading.Tasks;

namespace Orders.DataAccess.Interfaces
{
    public interface ICategoryRepository
    {
        Task<DataResult<Category>> Get(string id);
    }
}
