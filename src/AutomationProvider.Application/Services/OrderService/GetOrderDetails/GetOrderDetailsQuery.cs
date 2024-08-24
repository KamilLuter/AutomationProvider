using AutomationProvider.Domain.OrderAggregate;
using ErrorOr;
using MediatR;

namespace AutomationProvider.Application.Services.OrderService.GetOrderDetails
{
    public record GetOrderDetailsQuery(
        Guid OrderId,
        Guid CustomerId): IRequest<ErrorOr<Order>>;
}
