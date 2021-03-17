using Sample.Api.Customers.Contracts;
using Sample.Api.Customers.Repositories.Mysql.Enitites;
using System.Collections.Generic;

namespace Sample.Api.Customers.Repositories.Mysql.ModelFactory
{
    internal class ModelFactory
    {
        internal static CustomerModel CreateCustomerModel(CustomerEntity customerEntity)
        {
            return new CustomerModel()
            {
                Email = customerEntity.Email.ToLowerInvariant(),
                FirstName = customerEntity.FirstName,
                LastName = customerEntity.LastName,
                Gender = customerEntity.Gender,
            };
        }


        internal static List<CustomerModel> CreateCustomerModels(IEnumerable<CustomerEntity> customerEntities)
        {
            List<CustomerModel> customerModels = new List<CustomerModel>();
            foreach (var customerEntity in customerEntities)
            {
                customerModels.Add(CreateCustomerModel(customerEntity));
            }
            return customerModels;
        }
    }
}
