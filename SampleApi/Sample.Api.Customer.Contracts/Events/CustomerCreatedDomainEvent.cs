using Sample.Api.Common.Contracts.Events;
using Sample.Api.Customers.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Api.Customer.Contracts
{
    public class CustomerCreatedDomainEvent : BaseDomainEvent
    {
        public CustomerModel CustomerModel { get; set; }

        public CustomerCreatedDomainEvent(CustomerModel customerModel)
        {
            CustomerModel = customerModel;
        }
    }
}
