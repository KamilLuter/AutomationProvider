using AutomationProvider.Domain.Common.ValueObjects;

namespace AutomationProvider.Contracts.Products
{
    public record GetProductResponse(
        string Name
        , MoneyResponseContract Price
        , Dictionary<string, object> ProductAttributes
        , string Id
        , string Description
        , string CatalogName
        , string CatalogId);
}
