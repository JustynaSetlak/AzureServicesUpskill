using Autofac;
using Orders.EventHandler.Handlers;
using Orders.EventHandler.Interfaces;
using Orders.EventHandler.Providers;
using Orders.EventHandler.Services;

namespace Orders.EventHandler.DependencyModules
{
    public class EventDependencyModule : Module 
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrderEventsPublishService>().As<IOrderEventsPublishService>();
            builder.RegisterType<EventGridClientProvider>().As<IEventGridClientProvider>();
            builder.RegisterType<EventDispatchService>().As<IEventDispatchService>();

            builder
                .RegisterAssemblyTypes(typeof(IEventHandler<>).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
