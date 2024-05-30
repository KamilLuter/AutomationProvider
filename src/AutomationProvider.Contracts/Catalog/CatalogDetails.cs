using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Contracts.Catalog
{
    public record CatalogDetailsResponse(
        string Id
        , string Name
        , string Description
        , List<AttributesResponse> AvailableProductAttributes
        );
}
