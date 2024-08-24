using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Domain.OrderAggregate;
using ErrorOr;
using MediatR;
using AutomationProvider.Domain.Common.Errors;

namespace AutomationProvider.Application.Services.OrderService.GetOrderDetails
{
    public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, ErrorOr<Order>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderDetailsQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<ErrorOr<Order>> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderDetailsAsync(request.OrderId, cancellationToken);
            
            if (order is null)
                return Errors.Order.OrderNotFound;

            if (order.CustomerId != request.CustomerId)
                return Errors.Order.PermissionDenied;

            return order;
        }
    }
}
