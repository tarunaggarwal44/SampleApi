using Sample.Api.Customers.Contracts;
using Sample.Api.Customers.Repositories.Mysql.Enitites;
using System;

namespace Sample.Api.Customers.Repositories.Mysql.EntityFactory
{
    internal class EntityFactory
    {
        internal static CustomerEntity CreateCustomerEntity(CustomerModel customerModel)
        {
            return new CustomerEntity()
            {
                Email = customerModel.Email.ToLowerInvariant(),
                FirstName = customerModel.FirstName,
                LastName = customerModel.LastName,
                Gender = customerModel.Gender,
            };
        }

        internal static CustomerEntity UpdateCustomerEntity(CustomerModel customerModel)
        {
            var customerEntity = CreateCustomerEntity(customerModel);
            customerEntity.ModifiedDate = DateTime.UtcNow;
            return customerEntity;
        }
    }
}
