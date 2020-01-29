using System.Threading.Tasks;

namespace Orders.Infrastructure.InitConfiguration.Interfaces
{
    public interface IConfigurationHandler
    {
        Task Configure();
    }
}
