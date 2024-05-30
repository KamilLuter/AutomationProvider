using AutomationProvider.Application.Services.ProductService.Commands.Common;
using AutomationProvider.Domain.Product;
using ErrorOr;
using MediatR;

namespace AutomationProvider.Application.Services.ProductService.Queries.GetProductDetailedData
{
    public record GetProductByIdQuery(
        string ProductId
           ) : IRequest<ErrorOr<ProductResult>>;
}
