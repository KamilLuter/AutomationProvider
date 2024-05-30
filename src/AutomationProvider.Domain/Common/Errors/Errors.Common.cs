using ErrorOr;

namespace AutomationProvider.Domain.Common.Errors
{
    public partial class Errors
    {
        public static class Common
        {
            public static Error InvalidRequest => Error.Forbidden(
                code: "Common.InvalidRequest", description: "Invalid request");
            public static Error UnsupportedCurrency => Error.Forbidden(
                code: "Money.UnsupportedCurrency"
                , description: "Unsupported currency");

            public static Error InvalidAddress => Error.Forbidden(
                code: "Address.Invalid", description: "Invalid address parameters");
        }
    }
}
