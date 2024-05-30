using AutomationProvider.Application.Common.Enums;
using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Application.Services.ProductService.Commands.Common;
using AutomationProvider.Domain.CatalogAggregate;
using AutomationProvider.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace AutomationProvider.Application.Services.ProductService.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ErrorOr<IEnumerable<ProductResult>?>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICatalogRepository _catalogRepository;
        private const string CatalogId = "CatalogId";
        private const string NoCatalog = "All";
        public GetProductsQueryHandler(IProductRepository productRepository, ICatalogRepository catalogRepository)
        {
            _productRepository = productRepository;
            _catalogRepository = catalogRepository;
        }
        public async Task<ErrorOr<IEnumerable<ProductResult>?>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            if (request.query.HasNoParameters())
            {
                var result = await _productRepository.GetAllProductsAsync(cancellationToken);
                return result is not null ?
                    result.Select(p => new ProductResult(p, NoCatalog)).ToList()
                    : Errors.Product.NotFound;
            }                

            if (!request.query.HasCatalogParameter())
                return Errors.Product.CategoryNotProvided;

            var catalog = 
                await _catalogRepository
                        .GetCatalogByNameAsync(
                            request.query[nameof(Catalog)], cancellationToken);

            if (catalog is null)
                return Errors.Catalog.NotFound;

            if (!request.query.ValidateQuery(catalog.AvailableProductAttributes)) 
                return Errors.Product.WrongAttributeName;

            OrderBy orderBy = OrderBy.def;
            Enum.TryParse<OrderBy>(request.orderBy, true, out orderBy);

           var modifiedQuery = request.query;

            ReplaceCategoryNameWithId(modifiedQuery, catalog.Id);

            var products =
                await _productRepository
                        .GetProductsFromQuery(modifiedQuery, orderBy, request.page, request.pageSize, cancellationToken);

            return products is not null ? 
                products.Select(p => new ProductResult(p, catalog.Name)).ToList()
                : new List<ProductResult>();
        }

        private void ReplaceCategoryNameWithId(
            Dictionary<string, string> query, 
            Guid catalogId)
        {
            query.Remove(nameof(Catalog));
            query.Add(CatalogId, catalogId.ToString());
        }
    }
}
