using AutomationProvider.Application.Services.ProductService.Commands.Common;
using MediatR;

namespace AutomationProvider.Application.Services.ProductService.Queries.GetProductsByCatalog
{
    public record GetProductsByCatalogQuery(
        string CatalogId
           ) : IRequest<ProductResult>;
}
