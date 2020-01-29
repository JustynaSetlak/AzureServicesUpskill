using Autofac;
using Orders.BusinessLogic.Services;
using Orders.Storage.FileStorage.Providers;
using Orders.Storage.FileStorage.Providers.Interfaces;
using Orders.Storage.FileStorage.Repositories;
using Orders.Storage.FileStorage.Repositories.Interfaces;
using Orders.Storage.FileStorage.Services.Interfaces;
using Orders.Storage.QueueStorage.Providers;
using Orders.Storage.QueueStorage.Providers.Interfaces;

namespace Orders.Storage.DependencyModules
{
    public class StorageDependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BlobFileStorageRepository>().As<IBlobFileStorageRepository>();
            builder.RegisterType<BlobStorageClientProvider>().As<IBlobStorageClientProvider>();
            builder.RegisterType<ImageUploadService>().As<IImageUploadService>();
            builder.RegisterType<CloudQueueClientProvider>().As<ICloudQueueClientProvider>();
        }
    }
}
