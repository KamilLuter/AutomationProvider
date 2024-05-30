using AutomationProvider.Domain.Common.Models;
using AutomationProvider.Domain.Common.ValueObjects;
using AutomationProvider.Domain.OrderAggregate.Entities;
using AutomationProvider.Domain.Common.Errors;

using ErrorOr;
using System.Diagnostics.CodeAnalysis;

namespace AutomationProvider.Domain.Order
{
    public sealed class Order : AggregateRoot<Guid>
    {
        private readonly List<OrderLine> _orderLines = new List<OrderLine>();
        public Address ShippingAddress { get; private set; }
        public Guid? CustomerId { get; private set; }
        public DateTime CreatedAt { get; }
        public Money Price { get; private set; }
        public DateTime? DeliveredAt { get; private set; }
        public IReadOnlyList<OrderLine> OrderLines => _orderLines.AsReadOnly();

        private Order(Guid orderId
            , List<OrderLine> orderLines
            , Address shippingAddress
            , Guid? customerId
            , Money price
            , DateTime createdAt
            , DateTime? arrivedAt)
             : base(orderId)
        {
            _orderLines = orderLines;
            ShippingAddress = shippingAddress;
            CustomerId = customerId;
            CreatedAt = DateTime.Now;
            Price = price;
            CreatedAt = createdAt;
            DeliveredAt = null;
        }
        public static ErrorOr<Order> Create(
            List<OrderLine> orderLines
            , Address shippingAddress
            , Guid? customerId)
        {
            if (orderLines == null || orderLines.Count == 0)
                return Errors.Order.ZeroOrderLines;

            if (shippingAddress is null)
                return Errors.Order.NoAddressProvided;

            var price = Money.Create(
                orderLines.Sum(ol => ol.Price.Value * ol.Quantity)
                , orderLines.First().Price.Currency);

            if (price.IsError)
                return price.Errors;

            var orderId = Guid.NewGuid();
            var createdAt = DateTime.Now;

            return new Order(orderId, orderLines, shippingAddress, customerId, price.Value, createdAt, null);
        }

        public bool HasDifferentCurrencies()
        {
            return _orderLines.GroupBy(ol => ol.Price.Currency).Count() > 1;
        }

        #pragma warning disable CS8618
        protected Order()
        {
        }
        #pragma warning restore CS8618

    }
}
