using Autofac;
using Microsoft.Extensions.Hosting;
using Orders.BusinessLogic.Services.Interfaces;
using Orders.HostedServices;

namespace Orders.BusinessLogic.DependencyModules
{
    public class BusinessLogicDependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterServices(builder);

            builder.RegisterType<TagManagementHostedService>().As<IHostedService>();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(IService).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
