using Microsoft.WindowsAzure.Storage.Table;
using Orders.Results;
using Orders.TableStorage.Providers.Interfaces;
using Orders.TableStorage.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Orders.DataAccess.TableRepositories
{
    public class GenericTableRepository<T> : IGenericTableRepository<T> where T : class, ITableEntity
    {
        private readonly ITableClientProvider<T> _tableClientProvider;

        public GenericTableRepository(ITableClientProvider<T> tableClientProvider)
        {
            _tableClientProvider = tableClientProvider;
        }

        public async Task CreateTable()
        {
            await _tableClientProvider.CreateTable();
        }

        public async Task<DataResult<T>> Get(string partitionKey, string id)
        {
            var retrieve = TableOperation.Retrieve<T>(partitionKey, id);
            var result = await _tableClientProvider.ExecuteOperation(retrieve);

            return result;
        }

        public async Task<DataResult<T>> Insert(T element)
        {
            var insertOperation = TableOperation.Insert(element);
            var result = await _tableClientProvider.ExecuteOperation(insertOperation);

            return result;
        }

        public async Task<DataResult<T>> InsertOrMerge(T element)
        {
            var insertOrReplace = TableOperation.Insert(element);

            var result = await _tableClientProvider.ExecuteOperation(insertOrReplace);

            return result;
        }

        public async Task<DataResult<T>> Replace(T element)
        {
            var replace = TableOperation.Replace(element);
            var result = await _tableClientProvider.ExecuteOperation(replace);

            return result;
        }

        public async Task<DataResult<T>> Delete(T element)
        {
            var deleteOperation = TableOperation.Delete(element);
            var result = await _tableClientProvider.ExecuteOperation(deleteOperation);

            return result;
        }
    }
}
