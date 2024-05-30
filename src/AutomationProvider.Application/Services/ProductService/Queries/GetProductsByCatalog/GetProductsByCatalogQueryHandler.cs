using AutomationProvider.Application.Services.ProductService.Commands.Common;
using MediatR;

namespace AutomationProvider.Application.Services.ProductService.Queries.GetProductsByCatalog
{
    internal class GetProductsByCatalogQueryHandler :
        IRequestHandler<GetProductsByCatalogQuery, ProductResult>
    {
        Task<ProductResult> IRequestHandler<GetProductsByCatalogQuery, ProductResult>.Handle(GetProductsByCatalogQuery request, CancellationToken cancellationToken)
        {
            
            return null;
        }
    }
}
