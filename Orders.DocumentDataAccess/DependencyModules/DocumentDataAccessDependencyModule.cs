using Autofac;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Orders.DataAccess.Repositories.Interfaces;
using Orders.DocumentDataAccess.Handlers;
using Orders.DocumentDataAccess.Options;
using Orders.DocumentDataAccess.Repositories;
using Orders.Infrastructure.InitConfiguration.Interfaces;
using System;

namespace Orders.DocumentDataAccess.DependencyModules
{
    public class DocumentDataAccessDependencyModule : Module
    {
        private readonly OrdersDatabaseConfig _configuration;

        public DocumentDataAccessDependencyModule(OrdersDatabaseConfig configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(x => new DocumentClient(new Uri(_configuration.Url), _configuration.Key))
                .As<IDocumentClient>();

            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<DocumentDataAccessConfigurationHandler>().As<IConfigurationHandler>();
        }
    }
}
