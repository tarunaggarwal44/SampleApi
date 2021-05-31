using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sample.Api.Common;
using Sample.Api.Common.Contracts.Events;
using Sample.Api.Customer.Contracts;
using Sample.Api.Customer.Contracts.Events;
using Sample.Api.Customers.Contracts;
using Sample.Api.Customers.Contracts.Interfaces;
using Serilog;
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
            await this.mediator.Publish(baseDomainEvent);
            await this.mediator.Publish(baseDomainEvent1);


            Log.Information("Create Customer " + customerModel);
            var customerResponse = await customerBusiness.CreateCustomer(customerModel);
            return this.CreatePostHttpResponse(customerResponse);
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
