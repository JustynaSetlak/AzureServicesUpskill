using Orders.Search.Models;
using System.Threading.Tasks;

namespace Orders.Search.Interfaces
{
    public interface IOrderSearchService
    {
        Task MergeOrUpload(OrderUploadModel orderUploadModel);

        Task<OrderGetModel> Get(string id);
    }
}
