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
        public static class Review
        {
            public static Error NotFound => Error.NotFound(
                 code: "Review.NotFound",
                 description: "Review was not found");

            public static Error Unauthorized => Error.Unauthorized(
                code: "Review.Unauthorized",
                description: "Unauthorized opertion");
        }
    }
}
