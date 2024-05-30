using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Contracts.Products
{
    public record CreateProductRequest(
        string Name
        , string Description
        , string Manufacturer
        , MoneyResponseContract Price
        , Dictionary<string, object> ProductAttributes
        , Guid CatalogId);
}
