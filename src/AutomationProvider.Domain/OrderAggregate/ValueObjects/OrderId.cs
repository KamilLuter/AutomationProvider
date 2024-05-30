using AutomationProvider.Domain.Common.Models;

namespace AutomationProvider.Domain.OrderAggregate.ValueObjects
{
    public sealed class OrderId : ValueObjectId<OrderId>
    {
        private OrderId(Guid value) : base(value)
        {
        }
    }
}
