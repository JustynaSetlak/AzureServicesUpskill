using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.Functions.Options;
using Orders.Functions.Services;
using Orders.Functions.Services.Interfaces;

[assembly: FunctionsStartup(typeof(Orders.Functions.Startup))]
namespace Orders.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            RegisterServices(builder);
            AddOptions(builder);
        }

        private void RegisterServices(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<ICacheService, CacheService>();
            builder.Services.AddTransient<ITableStorageService, TableStorageService>();
            builder.Services.AddTransient<IImageService, ImageService>();
        }

        private void AddOptions(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<CacheOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(nameof(CacheOptions)).Bind(settings);
                });

            builder.Services.AddOptions<ResizeImageOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(nameof(ResizeImageOptions)).Bind(settings);
                });
        }
    }
}
