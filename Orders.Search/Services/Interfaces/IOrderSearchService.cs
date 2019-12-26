using Orders.Search.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.Search.Services.Interfaces
{
    public interface IOrderSearchService
    {
        Task MergeOrUpload(OrderUploadModel orderUploadModel);

        Task<OrderGetModel> Get(string id);

        Task<List<OrderGetModel>> Search(OrderSearchParamsModel orderSearchModel);
    }
}
