using System.Threading.Tasks;
using Orders.BusinessLogic.Dtos.Tag;
using Orders.BusinessLogic.Interfaces;
using Orders.DataAccess.TableRepositories.Models;
using Orders.Results;

namespace Orders.Services.Interfaces
{
    public interface ITagService : IService
    {
        Task<bool> Delete(string id);

        Task<bool> InsertOrMerge(UpsertTagDto upsertTag);

        Task<DataResult<Tag>> Get(string id);

        Task<DataResult<string>> InsertTag(CreateTagDto tagToAdd);

        Task<Task<bool>> HandleMarketingTagModificationRequest();
    }
}
