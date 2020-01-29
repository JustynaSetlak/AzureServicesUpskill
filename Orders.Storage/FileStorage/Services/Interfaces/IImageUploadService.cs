using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Orders.Storage.FileStorage.Services.Interfaces
{
    public interface IImageUploadService
    {
        Task<string> UploadFile(string fileName, IFormFile fileStream);

        Task RemoveFile(string blobUri);

        string GetImageMiniatureUrl(string fileName);
    }
}
