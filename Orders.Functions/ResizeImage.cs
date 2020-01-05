using System.IO;
using Microsoft.Azure.WebJobs;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Orders.Functions
{
    public static class ResizeImage
    {
        [FunctionName(nameof(ResizeImage))]
        public static void Run(
            [BlobTrigger("%BlobContainerName%/{name}")]Stream inputBlob, 
            [Blob("%BlobContainerName%/%ResizedImageNamePrefix%{name}", FileAccess.Write)] Stream outputBlob)
        {
            var newWidthToUse = 1200;
            var newHeightToUse = 800;

            using (var image = Image.Load(inputBlob))
            {
                image.Mutate(x => x.Resize(newWidthToUse, newHeightToUse));
                image.SaveAsJpeg(outputBlob);
            }
        }
    }
}
