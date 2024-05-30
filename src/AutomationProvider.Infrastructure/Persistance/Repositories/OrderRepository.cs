using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Domain.Order;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace AutomationProvider.Infrastructure.Persistance.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbSet<Order> _orders;
        public OrderRepository(AutomationProviderDbContext dbContext)
        {
            _orders = dbContext.Orders;
        }

        public async Task<Order?> CreateOrderAsync(Order order, CancellationToken cancellationToken)
        {
            var result = await _orders.AddAsync(order, cancellationToken);
            return result.Entity;
        }

        public async Task<Order?> GetOrderDetailsAsync(Guid orderId, CancellationToken cancellationToken)
        {
            return await _orders.Where(o => o.Id == orderId).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Order>?> GetOrdersByDateAsync(
            Guid customerId
            , DateTime fromDate
            , DateTime toDate
            , int page
            , int pageSize
            , CancellationToken cancellationToken)
        {
            return await _orders
                .Where(o => o.CustomerId == customerId 
                       && o.CreatedAt.Date >= fromDate
                       && o.CreatedAt.Date <= toDate)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }
    }
}
