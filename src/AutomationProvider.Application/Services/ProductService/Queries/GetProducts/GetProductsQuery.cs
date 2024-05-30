using AutomationProvider.Application.Services.ProductService.Commands.Common;
using AutomationProvider.Domain.Product;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Application.Services.ProductService.Queries.GetProducts
{
    public record GetProductsQuery(
        Dictionary<string, string> query,
        string orderBy,
        int page,
        int pageSize): IRequest<ErrorOr<IEnumerable<ProductResult>?>>;
}
