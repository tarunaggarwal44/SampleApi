using Sample.Api.Common.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Api.Common.Contracts
{
    public class BaseModel
    {
        public int Id { get; set; }

        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}
