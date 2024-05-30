using AutomationProvider.Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
