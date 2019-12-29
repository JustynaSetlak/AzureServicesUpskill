using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Orders.BusinessLogic.Interfaces
{
    public interface IImageUploadService : IService
    {
        Task<string> UploadFile(string fileName, IFormFile fileStream);

        Task RemoveFile(string blobUri);
    }
}
