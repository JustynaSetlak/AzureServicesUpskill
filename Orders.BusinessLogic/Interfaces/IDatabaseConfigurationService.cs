using System.Threading.Tasks;

namespace Orders.Configuration.Interfaces
{
    public interface IDatabaseConfigurationService
    {
        Task CreateDatabaseIfNotExist();
    }
}
