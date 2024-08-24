using AutomationProvider.Domain.OrderAggregate;

namespace AutomationProvider.Application.Common.Interfaces.Persistance
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderDetailsAsync(Guid orderId, CancellationToken cancellationToken);
        Task<List<Order>?> GetOrdersByDateAsync(Guid customerId
            , DateTime fromDate
            , DateTime toDate
            , int page
            , int pageSize
            , CancellationToken cancellationToken);
        Task<Order?> CreateOrderAsync(Order order, CancellationToken cancellationToken);
    }
}
