using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orders.Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Orders.HostedServices
{
    public class IndexingHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public IndexingHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var orderIndexProvider = scope.ServiceProvider.GetRequiredService<IOrderIndexProvider>();

                await orderIndexProvider.Create();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
