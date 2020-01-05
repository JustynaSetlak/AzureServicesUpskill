using System.IO;
using System.Threading.Tasks;

namespace Orders.DataAccess.Storage.Interfaces
{
    public interface IBlobFileStorage
    {
        Task<string> UploadFile(string fileName, Stream fileStream);

        Task RemoveFile(string fileName);

        string GetFileUri(string fileName);
    }
}
