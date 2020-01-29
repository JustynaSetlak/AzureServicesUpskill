using Autofac;
using Orders.DataAccess.TableRepositories;
using Orders.Infrastructure.InitConfiguration.Interfaces;
using Orders.TableStorage.Handlers;
using Orders.TableStorage.Providers.Interfaces;
using Orders.TableStorage.Repositories;
using Orders.TableStorage.Repositories.Interfaces;

namespace Orders.TableStorage.DependencyModules
{
    public class TableStorageDependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CategoryTableRepository>().As<ICategoryTableRepository>();
            builder.RegisterType<TagTableRepository>().As<ITagTableRepository>();
            builder.RegisterGeneric(typeof(GenericTableRepository<>)).As(typeof(IGenericTableRepository<>));
            builder.RegisterGeneric(typeof(TableClientProvider<>)).As(typeof(ITableClientProvider<>));
            builder.RegisterType<TableStoragConfigurationHandler>().As<IConfigurationHandler>();
        }

    }
}
