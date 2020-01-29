using System.IO;
using System.Threading.Tasks;

namespace Orders.Storage.FileStorage.Repositories.Interfaces
{
    public interface IBlobFileStorageRepository
    {
        Task<string> UploadFile(string fileName, Stream fileStream);

        Task RemoveFile(string fileName);

        string GetFileUri(string fileName);
    }
}
