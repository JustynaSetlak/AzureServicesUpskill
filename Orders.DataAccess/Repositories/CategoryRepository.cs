using Orders.DataAccess.Interfaces;
using Orders.Models;
using Orders.Repositories.Interfaces;
using Orders.Results;
using System.Threading.Tasks;

namespace Orders.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IBaseTableDbGenericRepository<Category> _categoryRepository;
        private readonly string categorPartitionKey = nameof(Category);

        public CategoryRepository(IBaseTableDbGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<DataResult<Category>> Get(string id)
        {
            var result = await _categoryRepository.Get(categorPartitionKey, id);

            return result;
        }
    }
}
