using Orders.DataAccess.Storage.Models;
using System.Threading.Tasks;

namespace Orders.Storage.QueueStorage.Services.Interfaces
{
    public interface ITagManagementQueueStorageService
    {
        Task<TagModification> RetrieveMessage();
    }
}
