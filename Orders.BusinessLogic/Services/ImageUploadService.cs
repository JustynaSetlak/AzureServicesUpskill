using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Orders.BusinessLogic.Interfaces;
using Orders.Common.Config;
using Orders.DataAccess.Storage.Interfaces;
using System.IO;
using System.Threading.Tasks;


namespace Orders.BusinessLogic.Services
{
    public class ImageUploadService : IImageUploadService
    {
        private readonly IBlobFileStorage _blobFileStorage;
        private readonly StorageConfig _storageConfig;

        public ImageUploadService(IBlobFileStorage blobFileStorage, IOptions<StorageConfig> storageConfigOptions)
        {
            _blobFileStorage = blobFileStorage;
            _storageConfig = storageConfigOptions.Value;
        }

        public async Task<string> UploadFile(string fileName, IFormFile uploadedFileStream)
        {
            var resultUri = await _blobFileStorage.UploadFile(fileName, uploadedFileStream.OpenReadStream());

            return resultUri;
        }

        public string GetImageMiniatureUrl(string fileName)
        {
            var miniatureImageName = $"{_storageConfig.ImageMiniatureNamePrefix}{fileName}";
            var resizedImageUrl = _blobFileStorage.GetFileUri(miniatureImageName);

            return resizedImageUrl;
        }

        public async Task RemoveFile(string blobUri)
        {
            string fileName = Path.GetFileName(blobUri);

            await _blobFileStorage.RemoveFile(fileName);
        }
    }
}
