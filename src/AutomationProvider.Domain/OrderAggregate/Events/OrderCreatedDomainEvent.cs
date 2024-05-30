using AutomationProvider.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Domain.OrderAggregate.Events
{
    public record OrderCreatedDomainEvent(
        Guid OrderId,
        Guid? UserId) : IDomainEvent; 
}
