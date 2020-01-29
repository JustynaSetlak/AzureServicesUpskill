using Orders.Infrastructure.InitConfiguration.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Infrastructure.InitConfiguration
{
    public class InitConfigurationHandler : IInitConfigurationHandler
    {
        private readonly IEnumerable<IConfigurationHandler> _handlers;

        public InitConfigurationHandler(IEnumerable<IConfigurationHandler> handlers)
        {
            _handlers = handlers;
        }

        public async Task ConfigureAll()
        {
            var taskList = _handlers.Select(x => x.Configure());

            await Task.WhenAll(taskList);
        }
    }
}
