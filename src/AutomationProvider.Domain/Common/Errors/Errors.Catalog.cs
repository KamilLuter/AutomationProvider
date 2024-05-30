using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Domain.Common.Errors
{
    public partial class Errors
    {
        public static class Catalog
        {
            public static Error NotFound => Error.NotFound(code: "Catalog.NotFound", description: "Catalog was not found");
        }
    }
}
