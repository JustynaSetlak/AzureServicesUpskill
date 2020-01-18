using Orders.Functions.Services.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace Orders.Functions.Services
{
    public class ImageService : IImageService
    {
        public void ResizeImage(int newWidth, int newHeight, Stream inputStream, Stream outputStream)
        {
            using (Image image = Image.Load(inputStream))
            {
                image.Mutate(x => x.Resize(newWidth, newHeight));
                image.SaveAsJpeg(outputStream);
            }
        }
    }
}
