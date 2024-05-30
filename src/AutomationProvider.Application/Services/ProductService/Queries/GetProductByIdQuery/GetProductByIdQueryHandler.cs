using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Application.Services.ProductService.Commands.Common;
using AutomationProvider.Domain.Common.Enums;
using AutomationProvider.Domain.Common.Errors;
using AutomationProvider.Domain.Common.ValueObjects;
using AutomationProvider.Domain.Product;
using ErrorOr;
using MediatR;

namespace AutomationProvider.Application.Services.ProductService.Queries.GetProductDetailedData
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ErrorOr<ProductResult>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICatalogRepository _catalogRepository;
        public GetProductByIdQueryHandler(IProductRepository productRepository, ICatalogRepository catalogRepository)
        {
            _productRepository = productRepository;
            _catalogRepository = catalogRepository;
        }
        public async Task<ErrorOr<ProductResult>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(new Guid(request.ProductId), cancellationToken);

            if (product is null)
            {
                return Errors.Product.NotFound;
            }

            var categoryName = await _catalogRepository.GetCatalogNameByIdAsync(product.CatalogId, cancellationToken);

            if (categoryName is null)
            {
                return Errors.Catalog.NotFound;
            }

            var result = new ProductResult(product, categoryName);
            return result;
        }
    }
}
