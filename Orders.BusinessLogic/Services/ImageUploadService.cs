using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Orders.BusinessLogic.Interfaces;
using Orders.Config;
using Orders.DataAccess.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;


namespace Orders.BusinessLogic.Services
{
    public class ImageUploadService : IImageUploadService
    {
        private readonly IBlobFileRepository _blobFileRepository;

        public ImageUploadService(IBlobFileRepository blobFileRepository)
        {
            _blobFileRepository = blobFileRepository;
        }

        public async Task<string> UploadFile(string fileName, IFormFile uploadedFileStream)
        {
            var resultUri = await _blobFileRepository.UploadFile(fileName, uploadedFileStream.OpenReadStream());

            return resultUri;
        }

        public async Task RemoveFile(string blobUri)
        {
            string fileName = Path.GetFileName(blobUri);

            await _blobFileRepository.RemoveFile(fileName);
        }
    }
}
