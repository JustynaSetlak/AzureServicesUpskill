using Microsoft.AspNetCore.Http;
using Orders.BusinessLogic.Interfaces;
using Orders.DataAccess.Storage.Interfaces;
using System.IO;
using System.Threading.Tasks;


namespace Orders.BusinessLogic.Services
{
    public class ImageUploadService : IImageUploadService
    {
        private readonly IBlobFileStorage _blobFileStorage;

        public ImageUploadService(IBlobFileStorage blobFileStorage)
        {
            _blobFileStorage = blobFileStorage;
        }

        public async Task<string> UploadFile(string fileName, IFormFile uploadedFileStream)
        {
            var resultUri = await _blobFileStorage.UploadFile(fileName, uploadedFileStream.OpenReadStream());

            return resultUri;
        }

        public async Task RemoveFile(string blobUri)
        {
            string fileName = Path.GetFileName(blobUri);

            await _blobFileStorage.RemoveFile(fileName);
        }
    }
}
