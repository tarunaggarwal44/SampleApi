using Microsoft.Extensions.DependencyInjection;
using Sample.Api.Common.Contracts.Constants;
using Sample.Api.Customers.Business;
using Sample.Api.Customers.Contracts.Interfaces;
using Sample.Api.Customers.Repositories.Mysql;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Sample.Api.Customers.Injections
{
    public class CustomerInjection
    {
        public static void CustomerBusinessInjections(IServiceCollection services)
        {
            services.AddTransient<ICustomerBusiness, CustomerBusiness>();
        }


        public static void CustomerRepositoryInjections(IServiceCollection services, Dictionary<string, string> appConfigurations)
        {
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IDbConnection>((sp) => new SqlConnection(appConfigurations[AppConfigurations.SampleDatabaseConnectionString]));
        }
    }
}
