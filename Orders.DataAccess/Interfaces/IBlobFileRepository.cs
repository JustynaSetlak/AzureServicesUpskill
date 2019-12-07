using System.IO;
using System.Threading.Tasks;

namespace Orders.DataAccess.Interfaces
{
    public interface IBlobFileRepository
    {
        Task<string> UploadFile(string fileName, Stream fileStream);

        Task RemoveFile(string fileName);
    }
}
