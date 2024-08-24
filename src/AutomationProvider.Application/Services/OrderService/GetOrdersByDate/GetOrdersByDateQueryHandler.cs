using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Application.Common.Interfaces.Services;
using AutomationProvider.Application.Services.OrderService.GetOrderDetails;
using AutomationProvider.Domain.Common.Errors;
using AutomationProvider.Domain.OrderAggregate;
using ErrorOr;
using MediatR;
using System;

namespace AutomationProvider.Application.Services.OrderService.GetOrdersByDate
{
    public class GetOrdersByDateQueryHandler : IRequestHandler<GetOrdersByDateQuery, ErrorOr<List<Order>?>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly int _pageSize = 10;
        public GetOrdersByDateQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ErrorOr<List<Order>?>> Handle(GetOrdersByDateQuery request, CancellationToken cancellationToken)
        {
            if (request.Page < 0)
                return Errors.Order.NegativePageNumber;

            if (request.FromDate > request.ToDate)
                return Errors.Order.WrongDates;

            var orders = await _orderRepository.GetOrdersByDateAsync(
                request.CustomerId
                , request.FromDate
                , request.ToDate
                , request.Page
                , _pageSize
                , cancellationToken);

            return orders;
        }
    }
}
