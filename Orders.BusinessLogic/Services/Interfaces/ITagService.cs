using System.Threading.Tasks;
using Orders.BusinessLogic.Dtos.Tag;
using Orders.Results;

namespace Orders.BusinessLogic.Services.Interfaces
{
    public interface ITagService : IService
    {
        Task<bool> Delete(string id);

        Task<bool> InsertOrMerge(UpsertTagDto upsertTag);

        Task<DataResult<DetailsTagDto>> Get(string id);

        Task<DataResult<string>> InsertTag(CreateTagDto tagToAdd);

        Task<Task<bool>> HandleMarketingTagModificationRequest();
    }
}
