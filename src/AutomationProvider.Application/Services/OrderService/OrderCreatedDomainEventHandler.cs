using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Domain.OrderAggregate.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Application.Services.OrderService
{
    public sealed class OrderCreatedDomainEventHandler :
        INotificationHandler<OrderCreatedDomainEvent>
    {
        private readonly IOrderRepository _orderRepository;
        public OrderCreatedDomainEventHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderDetailsAsync(notification.OrderId, cancellationToken);
            return;
        }
    }
}
