using Autofac;
using Orders.Infrastructure.InitConfiguration;
using Orders.Infrastructure.InitConfiguration.Interfaces;

namespace Orders.Infrastructure.DependencyModules
{
    public class InfrastructureDependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InitConfigurationHandler>().As<IInitConfigurationHandler>();
        }
    }
}
