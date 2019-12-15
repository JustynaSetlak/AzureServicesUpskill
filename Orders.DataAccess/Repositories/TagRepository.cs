using Orders.DataAccess.Interfaces;
using Orders.Models;
using Orders.Repositories.Interfaces;
using Orders.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Orders.DataAccess.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly IBaseTableDbGenericRepository<Tag> _tagRepository;
        private readonly string categorPartitionKey = nameof(Tag);

        public TagRepository(IBaseTableDbGenericRepository<Tag> tagRepository)
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
