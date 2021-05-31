using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Api.Common.Contracts.Events
{
    public abstract class BaseDomainEvent : INotification
    {
        private DateTime dateOccurred;
        public BaseDomainEvent()
        {
            dateOccurred = DateTime.UtcNow;
        }

        public DateTime DateOccurred
        {
            get
            {
                return this.dateOccurred;
            }
        }
    }
}
