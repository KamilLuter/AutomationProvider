using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Contracts.Orders
{
    public record GetOrdersByDateRequest(
        string? from
        , string? to
        , int page);
}
