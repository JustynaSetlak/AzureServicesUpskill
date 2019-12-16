using System.Threading.Tasks;

namespace Orders.Search.Interfaces
{
    public interface IOrderIndexProvider
    {
        Task Create();
    }
}
