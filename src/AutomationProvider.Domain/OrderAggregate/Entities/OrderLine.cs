using AutomationProvider.Domain.Common.Errors;
using AutomationProvider.Domain.Common.Models;
using AutomationProvider.Domain.Common.ValueObjects;
using ErrorOr;

namespace AutomationProvider.Domain.OrderAggregate.Entities
{
    public sealed class OrderLine : Entity<Guid>
    {
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public Money Price { get; private set; }
        private OrderLine(
            Guid id
            , Guid productId
            , int quality
            , Money price) : base(id)
        {
            ProductId = productId;
            Quantity = quality;
            Price = price;
        }

        public static ErrorOr<OrderLine> Create(Guid productId, int quantity, Money price)
        {
            if (quantity <= 0)
            {
                return Errors.Order.WrongQuantity;
            }

            var orderLineId = Guid.NewGuid();

            return new OrderLine(orderLineId, productId, quantity, price);
        }

        #pragma warning disable CS8618
        protected OrderLine()
        {
        }
        #pragma warning restore CS8618
}
}
