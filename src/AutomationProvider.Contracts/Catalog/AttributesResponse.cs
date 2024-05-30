using AutomationProvider.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Contracts.Catalog
{
    public record AttributesResponse(
        string NameWithUnits
        , List<string> AvailableValues
        , bool IsRangeAttribute
        , string Type);
}
