using Dapper;
using System;

namespace Sample.Api.Customers.Repositories.Mysql.Enitites
{
    [Table("customer")]
    public class CustomerEntity
    {
        [Key]
        public string Email { get; set; }

        public string FirstName { get; set; }


        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
