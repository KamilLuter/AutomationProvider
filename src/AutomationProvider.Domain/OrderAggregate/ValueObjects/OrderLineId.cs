using AutomationProvider.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Domain.OrderAggregate.ValueObjects
{
    public sealed class OrderLineId : ValueObject
    {
        public Guid Value { get; }
        private OrderLineId(Guid value)
        {
            Value = value;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        public static OrderLineId CreateUnique()
        {
            return new OrderLineId(Guid.NewGuid());
        }
    }
}
