﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.EventHandler.Options;

namespace Orders.Api.Configuration
{
    public static class SignalRServiceConfig
    {
        public static void AddSignalRService(this IServiceCollection services, IConfiguration configuration)
        {
            var sigalRConfig = configuration
                .GetSection(nameof(SignalRConfig))
                .Get<SignalRConfig>();

            services.AddSignalR().AddAzureSignalR(sigalRConfig.ConnectionString);
        }
    }
}
