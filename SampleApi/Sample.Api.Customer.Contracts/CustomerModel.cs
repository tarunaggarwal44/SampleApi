using System.ComponentModel;

namespace Sample.Api.Customers.Contracts
{
    public class CustomerModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Gender { get; set; }


        [DefaultValue("a@gmail.com")]
        public string Email { get; set; }

    }
}
