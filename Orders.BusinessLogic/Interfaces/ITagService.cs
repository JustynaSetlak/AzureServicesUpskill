using System.Threading.Tasks;
using Orders.Dtos.Tag;
using Orders.Models;
using Orders.Results;

namespace Orders.Services.Interfaces
{
    public interface ITagService
    {
        Task<bool> Delete(string id);
        Task<bool> UpdateDescription(UpdateTagDto tag);
        Task<Result<Tag>> Get(string id);
        Task<Result<string>> InsertTag(CreateTagDto tagToAdd);
    }
}
