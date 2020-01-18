using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.Functions.Services.Interfaces
{
    public interface ITableStorageService
    {
        Task<List<T>> QueryAllData<T>(CloudTable cloudTable) where T : ITableEntity, new();
    }
}
