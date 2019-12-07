using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Orders.Config;
using Orders.DataAccess.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Orders.DataAccess.Repositories
{
    public class BlobFileRepository : IBlobFileRepository
    {
        private readonly StorageConfig _storageConfig;

        public BlobFileRepository(IOptions<StorageConfig> storageConfigOptions)
        {
            _storageConfig = storageConfigOptions.Value;
        }

        public async Task<string> UploadFile(string fileName, Stream uploadedFileStream)
        {
            try
            {
                //var credential  = new StorageCredentials(_storageConfig.AccountName, _storageConfig.AccountKey);

                var storageAccount = CloudStorageAccount.DevelopmentStorageAccount;

                var client = storageAccount.CreateCloudBlobClient();

                var container = client.GetContainerReference(_storageConfig.ImageContainer);
                await container.CreateIfNotExistsAsync();
                await container.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });

                var blob = container.GetBlockBlobReference(fileName);
                await blob.UploadFromStreamAsync(uploadedFileStream);
                return blob.Uri.AbsoluteUri;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public async Task RemoveFile(string fileName)
        {
            try
            {

                //var credential  = new StorageCredentials(_storageConfig.AccountName, _storageConfig.AccountKey);

                var storageAccount = CloudStorageAccount.DevelopmentStorageAccount;

                var client = storageAccount.CreateCloudBlobClient();

                var container = client.GetContainerReference(_storageConfig.ImageContainer);

                var blob = container.GetBlockBlobReference(fileName);

                await blob.DeleteAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
