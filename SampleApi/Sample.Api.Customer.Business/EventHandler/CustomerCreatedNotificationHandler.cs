using MediatR;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Newtonsoft.Json;
using Sample.Api.Customer.Contracts;
using Sample.Api.Customers.Contracts.Interfaces;
using Serilog;
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
            Log.Debug("CustomerCreatedNotificationHandler started" + DateTime.UtcNow.ToString());
            string topicEndpoint = "https://demotopic.westus2-1.eventgrid.azure.net/api/events";

            // TODO: Enter value for <topic-key>. You can find this in the "Access Keys" section in the
            // "Event Grid Topics" blade in Azure Portal.
            string topicKey = "gJmktgoe6OFcrMPKT/4WvZcIBrKZt5Zz08K8ZmYwy2s=";

            string topicHostname = new Uri(topicEndpoint).Host;
            TopicCredentials topicCredentials = new TopicCredentials(topicKey);
            EventGridClient client = new EventGridClient(topicCredentials);

            await client.PublishEventsAsync(topicHostname, GetEventsList());
            Log.Debug("CustomerCreatedNotificationHandler ended" + DateTime.UtcNow.ToString());
        }


        private IList<EventGridEvent> GetEventsList()
        {
            List<EventGridEvent> eventsList = new List<EventGridEvent>();

            for (int i = 0; i < 1; i++)
            {
                eventsList.Add(new EventGridEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    EventType = "Contoso.Items.ItemReceived",
                    Data = new ContosoItemReceivedEventData()
                    {
                        ItemSku = "0"
                    },
                    EventTime = DateTime.Now,
                    Subject = "Door1",
                    DataVersion = "2.0"
                });
            }

            return eventsList;
        }

        class ContosoItemReceivedEventData
        {
            [JsonProperty(PropertyName = "itemSku")]
            public string ItemSku { get; set; }
        }
    }
}
