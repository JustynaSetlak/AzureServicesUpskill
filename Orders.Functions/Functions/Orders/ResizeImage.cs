using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Options;
using Orders.Functions.Options;
using Orders.Functions.Services.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Orders.Functions
{
    public class ResizeImage
    {
        private readonly IImageService _imageService;
        private readonly ResizeImageOptions _resizeImageOptions;

        public ResizeImage(IImageService imageService, IOptions<ResizeImageOptions> resizeImageOptions)
        {
            _imageService = imageService;
            _resizeImageOptions = resizeImageOptions.Value;
        }

        [FunctionName(nameof(ResizeImage))]
        public void Run(
            [BlobTrigger("%BlobContainerName%/{name}")]Stream inputBlob, 
            [Blob("%BlobContainerName%/%ResizedImageNamePrefix%{name}", FileAccess.Write)] Stream outputBlob)
        {
            _imageService.ResizeImage(_resizeImageOptions.Width, _resizeImageOptions.Height, inputBlob, outputBlob);
        }
    }
}
