using AutomationProvider.Application.Common.Enums;
using AutomationProvider.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Application.Common.Interfaces.Persistance
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>?> GetAllProductsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Product>?> GetProductsByCategory(Guid categoryId, CancellationToken cancellationToken);
        Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Product?> GetProductByNameAsync(string name, CancellationToken cancellationToken);
        Task<Product?> AddProductAsync(Product product, CancellationToken cancellationToken);
        Task<Product?> UpdateProductAsync(Product product, CancellationToken cancellationToken);
        Task DeleteProductAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Product>?> GetProductsFromQuery(
            Dictionary<string, string> query,
            OrderBy orderBy,
            int page,
            int pageSize,
            CancellationToken cancellationToken);
    }
}
