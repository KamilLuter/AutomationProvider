using AutomationProvider.Application.Common.Enums;
using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Application.Common.Interfaces.Services;
using AutomationProvider.Domain.Product;
using AutomationProvider.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Infrastructure.Persistance.Repositories
{
    public class CachedProductRepository : IProductRepository
    {
        private readonly ProductRepository _decorated;
        private readonly IMemoryCache _memoryCache;
        private readonly IDateTimeProvider _dateTimeProvider;
        public CachedProductRepository(ProductRepository decorated, IMemoryCache cache, IDateTimeProvider dateTimeProvider)
        {
            _decorated = decorated;
            _memoryCache = cache;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            string key = $"product-id-{id}";

            return await _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(_dateTimeProvider.UtcNow.AddMinutes(2));
                    return _decorated.GetProductByIdAsync(id, cancellationToken);
                });
        }

        public async Task<Product?> GetProductByNameAsync(string name, CancellationToken cancellationToken)
        {
            string key = $"product-name-{name}";

            return await _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(_dateTimeProvider.UtcNow.AddMinutes(2));
                    return _decorated.GetProductByNameAsync(name, cancellationToken);
                });
        }

        public async Task<Product?> AddProductAsync(Product product, CancellationToken cancellationToken)
        {
            return await _decorated.AddProductAsync(product, cancellationToken);
        }

        public async Task DeleteProductAsync(Guid id, CancellationToken cancellationToken)
        {
            await _decorated.DeleteProductAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Product>?> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            return await _decorated.GetAllProductsAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>?> GetProductsByCategory(Guid categoryId, CancellationToken cancellationToken)
        {
            return await _decorated.GetProductsByCategory(categoryId, cancellationToken);
        }

        public async  Task<IEnumerable<Product>?> GetProductsFromQuery(
            Dictionary<string, string> query, 
            OrderBy orderBy, 
            int page, 
            int pageSize, 
            CancellationToken cancellationToken)
        {
            return await _decorated.GetProductsFromQuery(query, orderBy, page, pageSize, cancellationToken);
        }

        public async Task<Product?> UpdateProductAsync(Product product, CancellationToken cancellationToken)
        {
            return await _decorated.UpdateProductAsync(product, cancellationToken);
        }
    }
}
