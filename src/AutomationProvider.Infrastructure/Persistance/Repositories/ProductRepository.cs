using AutomationProvider.Application.Common.Enums;
using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Domain.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationProvider.Infrastructure.Persistance.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbSet<Product> _products;
        private ProductsSqlBuilder _sqlBuilder;

        public ProductRepository(
            AutomationProviderDbContext dbContext,
            ProductsSqlBuilder sqlBuilder)
        {
            _products = dbContext.Products;
            _sqlBuilder = sqlBuilder;
        }
 
        public async Task<IEnumerable<Product>?> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            return await _products.ToListAsync(cancellationToken);
        }

        public async Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _products.FindAsync(id, cancellationToken);
        }

        public async Task<Product?> AddProductAsync(Product product, CancellationToken cancellationToken)
        {
            await _products.AddAsync(product, cancellationToken);
            return product;
        }

        public async Task<Product?> UpdateProductAsync(Product product, CancellationToken cancellationToken)
        {
            _products.Update(product);
            return await Task.FromResult(product);
        }

        public async Task DeleteProductAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _products.FindAsync(id, cancellationToken);
            if (product is not null)
            {
                _products.Remove(product);
            }
        }

        public async Task<IEnumerable<Product>?> GetProductsFromQuery(
            Dictionary<string, string> query,
            OrderBy orderBy,
            int page,
            int pageSize,
            CancellationToken cancellationToken)
        {
            foreach (var kvp in query)
            {
                _sqlBuilder = _sqlBuilder.AppendQueryCondition(kvp.Key, kvp.Value);
            }

            var sqlQuery = _sqlBuilder.Build();

            var queryable = _products.FromSqlRaw(sqlQuery);

            switch (orderBy)
            {
                case OrderBy.price_desc:
                    queryable = queryable.OrderByDescending(p => p.Price.Value);
                    break;
                case OrderBy.price_asc:
                    queryable = queryable.OrderBy(p => p.Price.Value);
                    break;
            };

            queryable = queryable
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            return await queryable.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>?> GetProductsByCategory(Guid categoryId, CancellationToken cancellationToken)
        {
            return await _products.Where(p => p.CatalogId == categoryId).ToListAsync(cancellationToken);
        }

        public async Task<Product?> GetProductByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _products.Where(p => p.Name == name).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
