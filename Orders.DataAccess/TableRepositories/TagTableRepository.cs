using Orders.DataAccess.TableRepositories.Interfaces;
using Orders.Models;
using Orders.Results;
using System.Threading.Tasks;

namespace Orders.DataAccess.TableRepositories
{
    public class TagTableRepository : ITagTableRepository
    {
        private readonly IGenericTableRepository<Tag> _tagRepository;
        private readonly string categorPartitionKey = nameof(Tag);

        public TagTableRepository(IGenericTableRepository<Tag> tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<DataResult<Tag>> Get(string id)
        {
            var result = await _tagRepository.Get(categorPartitionKey, id);

            return result;
        }
    }
}
