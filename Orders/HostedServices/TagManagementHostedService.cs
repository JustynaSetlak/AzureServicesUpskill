using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orders.DataAccess.Storage.Interfaces;
using Orders.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Orders.HostedServices
{
    public class TagManagementHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public TagManagementHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var tagService = scope.ServiceProvider.GetRequiredService<ITagService>();

                await tagService.HandleMarketingTagModificationRequest();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
