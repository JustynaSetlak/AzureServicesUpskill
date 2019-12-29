using Orders.DataAccess.TableRepositories.Interfaces;
using Orders.Models;
using Orders.Results;
using System.Threading.Tasks;

namespace Orders.DataAccess.TableRepositories
{
    public class CategoryTableRepository : ICategoryTableRepository
    {
        private readonly IGenericTableRepository<Category> _categoryRepository;
        private readonly string categorPartitionKey = nameof(Category);

        public CategoryTableRepository(IGenericTableRepository<Category> categoryRepository)
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
