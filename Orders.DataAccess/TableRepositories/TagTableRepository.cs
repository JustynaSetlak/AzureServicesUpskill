using Orders.DataAccess.TableRepositories.Interfaces;
using Orders.DataAccess.TableRepositories.Models;
using Orders.Results;
using System.Threading.Tasks;

namespace Orders.DataAccess.TableRepositories
{
    public class TagTableRepository : ITagTableRepository
    {
        private readonly IGenericTableRepository<Tag> _tableRepository;
        private readonly string categorPartitionKey = nameof(Tag);

        public TagTableRepository(IGenericTableRepository<Tag> tagRepository)
        {
            _tableRepository = tagRepository;
        }

        public async Task<DataResult<Tag>> Get(string id)
        {
            var result = await _tableRepository.Get(categorPartitionKey, id);

            return result;
        }

        public async Task<DataResult<Tag>> InsertOrMerge(Tag element)
        {
            var result = await _tableRepository.InsertOrMerge(element);

            return result;
        }

        public async Task<DataResult<Tag>> Delete(Tag element)
        {
            var result = await _tableRepository.Delete(element);

            return result;
        }
    }
}
