using AutomationProvider.Application.Idempotency;
using AutomationProvider.Contracts.Orders;
using AutomationProvider.Domain.OrderAggregate;
using ErrorOr;

namespace AutomationProvider.Application.Services.OrderService.CreateOrder
{
    public record CreateOrderCommand(
        Guid requestId
        , Guid? UserId
        , AddressContract Address
        , PaymentCommand PaymentDetails
        , MoneyCommand Sum
        , List<OrderLineCommand> OrderLines): IdempotentCommand<ErrorOr<Order>>(requestId);

    public record OrderLineCommand(
        string ProductId, 
        int Quantity,
        MoneyCommand Price);

    public record PaymentCommand(
        string PaymentMethod,
        string CardNumber);

    public record MoneyCommand(
        decimal Value,
        string Currency);
}
