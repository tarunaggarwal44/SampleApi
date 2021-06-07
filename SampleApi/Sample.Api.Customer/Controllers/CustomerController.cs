using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Sample.Api.Common;
using Sample.Api.Common.Contracts.Events;
using Sample.Api.Customer.Contracts;
using Sample.Api.Customer.Contracts.Events;
using Sample.Api.Customers.Contracts;
using Sample.Api.Customers.Contracts.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sample.Api.Customers.Controllers
{
    //[Route("api/v1/customers")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseApiController
    {
        private readonly IMediator mediator;
        private readonly ICustomerBusiness customerBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerController"/> class. 
        /// </summary>
        /// <param name="customerBusiness">customer Business</param>
        /// <param name="customerRepository">The customer repository</param>
        public CustomerController(ICustomerBusiness customerBusiness, IMediator mediator)
        {
            this.customerBusiness = customerBusiness;
            this.mediator = mediator;
        }

        //GET: api/<CustomerController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Log.Information("Get all Customers");
            var customerResponse = await customerBusiness.GetAllCustomers();
            return this.CreateGetHttpResponse(customerResponse);
        }


        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            Log.Information("Get Customer Email " + email);
            var customerResponse = await customerBusiness.GetCustomer(email);
            return this.CreateGetHttpResponse(customerResponse);
        }


        // POST api/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerModel customerModel)
        {
            BaseDomainEvent baseDomainEvent = new CustomerCreatedDomainEvent(customerModel);
            BaseDomainEvent baseDomainEvent1 = new CustomerCreatedDomainEvent1(customerModel);



            string topicEndpoint = "https://demotopic.westus2-1.eventgrid.azure.net/api/events";

            string topicKey = "gJmktgoe6OFcrMPKT/4WvZcIBrKZt5Zz08K8ZmYwy2s=";

            string topicHostname = new Uri(topicEndpoint).Host;
            TopicCredentials topicCredentials = new TopicCredentials(topicKey);
            EventGridClient client = new EventGridClient(topicCredentials);

            await client.PublishEventsAsync(topicHostname, GetEventsList(baseDomainEvent));


            var customerResponse = await customerBusiness.CreateCustomer(customerModel);
            return this.CreatePostHttpResponse(customerResponse);
        }


        private IList<EventGridEvent> GetEventsList(BaseDomainEvent baseDomainEvent)
        {
            List<EventGridEvent> eventsList = new List<EventGridEvent>();
            eventsList.Add(new EventGridEvent()
            {
                Id = Guid.NewGuid().ToString(),
                EventType = "Contoso.Items.ItemReceived",
                Data = baseDomainEvent,
                EventTime = DateTime.Now,
                Subject = "Door1",
                DataVersion = "2.0"
            });

            return eventsList;
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{email}")]
        public async Task<IActionResult> Put([FromBody] CustomerModel customerModel)
        {
            Log.Information("Update Customer " + customerModel);
            var customerResponse = await customerBusiness.UpdateCustomer(customerModel);
            return this.CreatePutHttpResponse(customerResponse);

        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            Log.Information("Delete Customer " + email);
            var customerResponse = await customerBusiness.DeleteCustomer(email);
            return this.CreateDeleteHttpResponse(customerResponse);
        }
    }
}
