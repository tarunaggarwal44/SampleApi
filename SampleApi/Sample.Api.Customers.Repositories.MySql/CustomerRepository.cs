using Dapper;
using Sample.Api.Common.Contracts;
using Sample.Api.Customers.Contracts;
using Sample.Api.Customers.Contracts.Interfaces;
using Sample.Api.Customers.Repositories.Mysql.Enitites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Sample.Api.Customers.Repositories.Mysql
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnection connection;

        public CustomerRepository(IDbConnection connection)
        {
            this.connection = connection;
        }
        public async Task<Response<string>> CreateCustomer(CustomerModel customerModel)
        {

            var parameters = new
            {
                Email = customerModel.Email,
                FirstName = customerModel.FirstName,
                LastName = customerModel.LastName,
                Gender = customerModel.Gender,
                ModifiedDateUtc = DateTime.UtcNow
            };
            var sql = @"INSERT INTO sample.customer (Email, FirstName, LastName, Gender, ModifiedDate)
VALUES(@Email, @FirstName, @LastName, @Gender, @ModifiedDateUtc); ";

            await this.connection.QueryAsync<CustomerEntity>(sql, parameters);

            return new Response<string>() { Result = customerModel.Email };

        }

        public async Task<Response<bool>> DeleteCustomer(string email)
        {
            await this.connection.DeleteAsync<CustomerEntity>(email.ToLowerInvariant());
            return new Response<bool>() { Result = true };
        }

        public async Task<Response<IEnumerable<CustomerModel>>> GetAllCustomers()
        {
            var customerEntities = await this.connection.GetListAsync<CustomerEntity>();
            var customerModels = ModelFactory.ModelFactory.CreateCustomerModels(customerEntities);
            if (customerModels.Count == 0)
            {
                return new Response<IEnumerable<CustomerModel>>();
            }

            return new Response<IEnumerable<CustomerModel>>() { Result = customerModels };
        }

        public async Task<Response<CustomerModel>> GetCustomer(string email)
        {
            var customerEntity = await this.connection.GetAsync<CustomerEntity>(email);
            if (customerEntity == null)
            {
                return new Response<CustomerModel>();
            }

            var customerModel = ModelFactory.ModelFactory.CreateCustomerModel(customerEntity);
            return new Response<CustomerModel>() { Result = customerModel };
        }

        public async Task<Response<bool>> UpdateCustomer(CustomerModel customerModel)
        {
            var customerEntity = EntityFactory.EntityFactory.UpdateCustomerEntity(customerModel);
            await this.connection.UpdateAsync<CustomerEntity>(customerEntity);
            return new Response<bool>() { Result = true };
        }
    }
}
