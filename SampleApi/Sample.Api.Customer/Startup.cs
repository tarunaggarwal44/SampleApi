using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sample.Api.Common.Contracts.Constants;
using Sample.Api.Customer.Business.EventHandler;
using Sample.Api.Customer.Contracts;
using Sample.Api.Customers;
using Sample.Api.Customers.Injections;
using System.Collections.Generic;
using System.Reflection;

namespace Sample.Api.Customer
{
    public static class Dependencies
    {
        public static IServiceCollection RegisterRequestHandlers(
            this IServiceCollection services)
        {
            return services
                .AddMediatR(typeof(Dependencies).Assembly);
        }
    }

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

            services.AddHttpContextAccessor();

            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddTransient<INotification, CustomerCreatedNotificationHandler> ();

            //services.AddMediatR(Assembly.GetExecutingAssembly());

            //services.RegisterRequestHandlers();


            Dictionary<string, string> appConfigurations = GetAppConfigurations();


            CustomerInjection.CustomerBusinessInjections(services);
            CustomerInjection.CustomerRepositoryInjections(services, appConfigurations);


            services.AddControllers();


            services.AddOpenApiDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Sample Customer API";
                    document.Info.Description = "Sample Customer API to manage customer data";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "TA",
                        Email = string.Empty,
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "",
                    };
                };
            });


        


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.ConfigureGlobalExceptionHandler();

            app.UseRouting();

            app.UseOpenApi();
            app.UseSwaggerUi3();


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        private Dictionary<string, string> GetAppConfigurations()
        {
            Dictionary<string, string> appConfigurations = new Dictionary<string, string>();
            var sampleConnectionString = Configuration.GetConnectionString(AppConfigurations.SampleSlotDatabase);
            appConfigurations.Add(AppConfigurations.SampleDatabaseConnectionString, sampleConnectionString);

            return appConfigurations;
        }
    }
}
