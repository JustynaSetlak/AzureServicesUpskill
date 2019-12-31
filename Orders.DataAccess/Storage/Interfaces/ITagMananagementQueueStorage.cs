using Orders.DataAccess.Storage.Models;
using System.Threading.Tasks;

namespace Orders.DataAccess.Storage.Interfaces
{
    public interface ITagManagementQueueStorage
    {
        Task<TagModificationRequest> RetrieveMessage();
    }
}
