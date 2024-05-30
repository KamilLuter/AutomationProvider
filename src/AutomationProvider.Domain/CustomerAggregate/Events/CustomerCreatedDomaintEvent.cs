using AutomationProvider.Domain.Common.Models;
using AutomationProvider.Domain.CustomerAggregate.ValueObjects;

namespace AutomationProvider.Domain.CustomerAggregate.Events
{
    public sealed record CustomerCreatedDomaintEvent(
        Guid CustomerId): IDomainEvent;    
}
