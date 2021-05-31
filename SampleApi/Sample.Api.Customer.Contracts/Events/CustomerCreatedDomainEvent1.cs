using Sample.Api.Common.Contracts.Events;
using Sample.Api.Customers.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Api.Customer.Contracts.Events
{
    public class CustomerCreatedDomainEvent1 : BaseDomainEvent
    {
        public CustomerModel CustomerModel { get; set; }

        public CustomerCreatedDomainEvent1(CustomerModel customerModel)
        {
            CustomerModel = customerModel;
        }
    }
}
