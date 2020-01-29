using System.Threading.Tasks;
using Orders.Infrastructure.InitConfiguration.Interfaces;
using Orders.TableStorage.Repositories.Interfaces;

namespace Orders.TableStorage.Handlers
{
    public class TableStoragConfigurationHandler : IConfigurationHandler
    {
        private readonly ICategoryTableRepository _categoryTableRepository;
        private readonly ITagTableRepository _tagTableRepository;

        public TableStoragConfigurationHandler(ICategoryTableRepository categoryTableRepository, ITagTableRepository tagTableRepository)
        {
            _categoryTableRepository = categoryTableRepository;
            _tagTableRepository = tagTableRepository;
        }

        public async Task Configure()
        {
            await _tagTableRepository.CreateTable();
            await _categoryTableRepository.CreateTable();
        }
    }
}
