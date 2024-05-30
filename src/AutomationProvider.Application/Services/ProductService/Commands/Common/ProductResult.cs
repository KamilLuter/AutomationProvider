using AutomationProvider.Domain.Product;

namespace AutomationProvider.Application.Services.ProductService.Commands.Common
{
    public record ProductResult(
        Product Product
        , string CategoryName);
}
