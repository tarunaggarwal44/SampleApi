using Sample.Api.Common.Contracts;
using Sample.Api.Customer.Contracts;
using System.ComponentModel;

namespace Sample.Api.Customers.Contracts
{
    public class CustomerModel : BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Gender { get; set; }

        [DefaultValue("s@g.c")]
        public string Email { get; set; }

        public void CreateCustomer()
        {
            Events.Add(new CustomerCreatedDomainEvent(this));
        }
    }
}
