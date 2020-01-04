using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Orders.Functions
{
    public static class ResizeImage
    {
        [FunctionName(nameof(ResizeImage))]
        public static void Run([BlobTrigger("%BlobContainerName%/{name}")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}
