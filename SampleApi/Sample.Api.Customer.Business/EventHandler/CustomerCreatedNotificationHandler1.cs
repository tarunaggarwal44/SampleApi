using MediatR;
using Sample.Api.Customer.Contracts.Events;
using Sample.Api.Customers.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Api.Customer.Business.EventHandler
{
   
    public class CustomerCreatedNotificationHandler1 : INotificationHandler<CustomerCreatedDomainEvent1>
    {

        // In a REAL app you might want to use the Outbox pattern and a command/queue here...
        public CustomerCreatedNotificationHandler1(ICustomerRepository customerRepository)
        {

        }

        // configure a test email server to demo this works
        // https://ardalis.com/configuring-a-local-test-email-server
        public async Task Handle(CustomerCreatedDomainEvent1 domainEvent, CancellationToken cancellationToken)
        {


        }
    }
}
