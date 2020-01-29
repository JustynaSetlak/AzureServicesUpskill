using System.Threading.Tasks;

namespace Orders.Infrastructure.InitConfiguration.Interfaces
{
    public interface IInitConfigurationHandler
    {
        Task ConfigureAll();
    }
}
