using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Orders.Storage.FileStorage.Repositories.Interfaces;
using Orders.Storage.FileStorage.Services.Interfaces;
using Orders.Storage.Options;
using System.IO;
using System.Threading.Tasks;


namespace Orders.BusinessLogic.Services
{
    public class ImageUploadService : IImageUploadService
    {
        private readonly IBlobFileStorageRepository _blobFileStorage;
        private readonly StorageConfig _storageConfig;

        public ImageUploadService(IBlobFileStorageRepository blobFileStorage, IOptions<StorageConfig> storageConfigOptions)
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
