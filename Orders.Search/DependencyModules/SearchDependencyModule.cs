using Autofac;
using Orders.Infrastructure.InitConfiguration.Interfaces;
using Orders.Search.Handlers;
using Orders.Search.Interfaces;
using Orders.Search.Providers;
using Orders.Search.Services.Interfaces;

namespace Orders.Search.DependencyModules
{
    public class SearchDependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrderSearchService>().As<IOrderSearchService>();
            builder.RegisterType<SearchServiceClientProvider>().As<ISearchServiceClientProvider>();
            builder.RegisterGeneric(typeof(SearchService<>)).As(typeof(ISearchService<>));
            builder.RegisterType<SearchConfigurationHandler>().As<IConfigurationHandler>();
        }
    }
}
