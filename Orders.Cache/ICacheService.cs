using System.Threading.Tasks;

namespace Orders.BusinessLogic.Interfaces.Infrastructure
{
    public interface ICacheService
    {
        Task<T> GetValue<T>(string key) where T : class;
    }
}
