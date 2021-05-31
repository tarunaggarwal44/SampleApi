using MediatR;
using Sample.Api.Customer.Contracts;
using Sample.Api.Customers.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Api.Customer.Business.EventHandler
{
 
    public class CustomerCreatedNotificationHandler : INotificationHandler<CustomerCreatedDomainEvent>
    {

        // In a REAL app you might want to use the Outbox pattern and a command/queue here...
        public CustomerCreatedNotificationHandler()
        {
            
        }

        // configure a test email server to demo this works
        // https://ardalis.com/configuring-a-local-test-email-server
        public async Task Handle(CustomerCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {

            
        }
    }
}
