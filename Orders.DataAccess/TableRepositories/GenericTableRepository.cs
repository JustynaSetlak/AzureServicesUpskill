using Microsoft.Azure.Cosmos.Table;
using Orders.DataAccess.TableRepositories.Interfaces;
using Orders.Results;
using System.Threading.Tasks;

namespace Orders.DataAccess.TableRepositories
{
    public class GenericTableRepository<T> : IGenericTableRepository<T> where T : class, ITableEntity
    {
        private readonly ITableOperationExecutionRepository<T> _tableOperationExecutionRepository;

        public GenericTableRepository(ITableOperationExecutionRepository<T> tableOperationExecutionRepository)
        {
            _tableOperationExecutionRepository = tableOperationExecutionRepository;
        }

        public async Task<DataResult<T>> Get(string partitionKey, string id)
        {
            var retrieve = TableOperation.Retrieve<T>(partitionKey, id);
            var result = await _tableOperationExecutionRepository.ExecuteOperation(retrieve);

            return result;
        }

        public async Task<DataResult<T>> Insert(T element)
        {
            var insertOperation = TableOperation.Insert(element);
            var result = await _tableOperationExecutionRepository.ExecuteOperation(insertOperation);

            return result;
        }

        public async Task<DataResult<T>> InsertOrMerge(T element)
        {
            var insertOrReplace = TableOperation.Insert(element);

            var result = await _tableOperationExecutionRepository.ExecuteOperation(insertOrReplace);

            return result;
        }

        public async Task<DataResult<T>> Replace(T element)
        {
            var replace = TableOperation.Replace(element);
            var result = await _tableOperationExecutionRepository.ExecuteOperation(replace);

            return result;
        }

        public async Task<DataResult<T>> Delete(T element)
        {
            var deleteOperation = TableOperation.Delete(element);
            var result = await _tableOperationExecutionRepository.ExecuteOperation(deleteOperation);

            return result;
        }
    }
}
