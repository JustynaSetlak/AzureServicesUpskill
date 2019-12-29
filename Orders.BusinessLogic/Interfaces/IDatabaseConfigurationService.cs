using Orders.BusinessLogic.Interfaces;
using System.Threading.Tasks;

namespace Orders.Configuration.Interfaces
{
    public interface IDatabaseConfigurationService : IService
    {
        Task CreateDatabaseIfNotExist();
    }
}
