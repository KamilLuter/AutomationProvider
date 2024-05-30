using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Contracts.Products
{
    public record UpdateProductRequest(
        string Name
        , MoneyResponseContract Price
        , Dictionary<string, object> ProductAttributes
        , string Description
        , string CatalogName);
}
