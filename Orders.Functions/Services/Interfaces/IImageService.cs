using System.IO;

namespace Orders.Functions.Services.Interfaces
{
    public interface IImageService
    {
        void ResizeImage(int newWidth, int newHeight, Stream inputStream, Stream outputStream);
    }
}
