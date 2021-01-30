﻿using Sample.Api.Common.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Api.Customers.Contracts.Interfaces
{
    public interface ICustomerBusiness
    {
        Task<Response<string>> CreateCustomer(CustomerModel customerModel);
        Task<Response<bool>> DeleteCustomer(string email);
        Task<Response<IEnumerable<CustomerModel>>> GetAllCustomers();
        Task<Response<CustomerModel>> GetCustomer(string email);
        Task<Response<bool>> UpdateCustomer(CustomerModel customerModel);
    }
}
