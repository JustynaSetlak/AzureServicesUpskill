using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.BusinessLogic.Interfaces;
using Orders.BusinessLogic.Services;
using Orders.Config;
using Orders.Configuration;
using Orders.Configuration.Interfaces;
using Orders.DataAccess.Interfaces;
using Orders.DataAccess.Repositories;
using Orders.Repositories;
using Orders.Repositories.Interfaces;
using Orders.Services;
using Orders.Services.Interfaces;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Orders
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<ProductTableDbConfig>(Configuration.GetSection(nameof(ProductTableDbConfig)));
            services.Configure<OrdersDatabaseConfig>(Configuration.GetSection(nameof(OrdersDatabaseConfig)));
            services.Configure<StorageConfig>(Configuration.GetSection(nameof(StorageConfig)));

            services.AddScoped<IDatabaseConfigurationService, DatabaseConfigurationService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped(typeof(IBaseTableDbGenericRepository<>), typeof(BaseTableDbGenericRepository<>));
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IDatabaseConfigurationRepository, DatabaseConfigurationRepository>();
            services.AddScoped<IBlobFileRepository, BlobFileRepository>();
            services.AddScoped<IImageUploadService, ImageUploadService>();

            var ordersDatabaseConfig = Configuration
                .GetSection(nameof(OrdersDatabaseConfig))
                .Get<OrdersDatabaseConfig>();

            services.AddScoped<IDocumentClient>(x => new DocumentClient(new Uri(ordersDatabaseConfig.Url), ordersDatabaseConfig.Key));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.OperationFilter<FileUploadOperationConfig>();
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDatabaseConfigurationService databaseConfigurationService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            databaseConfigurationService.CreateDatabaseIfNotExist().GetAwaiter().GetResult();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
