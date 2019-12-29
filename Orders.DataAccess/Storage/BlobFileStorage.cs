using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Orders.Common.Config;
using Orders.DataAccess.Storage.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Orders.DataAccess.Storage
{
    public class BlobFileStorage : IBlobFileStorage
    {
        private readonly StorageConfig _storageConfig;
        private readonly CloudBlobClient _cloudBlobClient;

        public BlobFileStorage(IOptions<StorageConfig> storageConfigOptions, IBlobStorageClientProvider blobStorageClientProvider)
        {
            _storageConfig = storageConfigOptions.Value;
            _cloudBlobClient = blobStorageClientProvider.CreateBlobStorageProvider();
        }

        public async Task<string> UploadFile(string fileName, Stream uploadedFileStream)
        {
            try
            {
                var container = _cloudBlobClient.GetContainerReference(_storageConfig.ImageContainer);
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
                //log ex
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
