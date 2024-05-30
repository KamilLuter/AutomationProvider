using ErrorOr;

namespace AutomationProvider.Domain.Common.Errors
{
    public partial class Errors
    {
        public static class Order
        {
            public static Error WrongQuantity 
                => Error.Validation( code: "Order.WrongQuantity", description: "Quantity is equal to zero or is negative");

            public static Error ZeroOrderLines
                => Error.Validation(code: "Order.ZeroOrderLines", description: "Order has zero order lines");

            public static Error NoAddressProvided
                => Error.Validation(code: "Order.NoAddressProvided", description: "Address was not provided");

            public static Error ProductDoesntExsist
                => Error.Validation(code: "Order.ProductDoesntExsist", description: "Ordered product is not available");

            public static Error OrderNotFound
                => Error.NotFound(code: "Order.NotFound", description: "Order not found");

            public static Error PermissionDenied
                => Error.Unauthorized(code: "Order.Unathorized", description: "No permission to access the order");

            public static Error WrongDates
                => Error.Validation(code: "Order.WrongDate", description: "Wrong date formats");

            public static Error NegativePageNumber
                => Error.Validation(code: "Order.NegativePageNumber", description: "Negative page number");

            public static Error HasDifferentCurrencies
                => Error.Validation(code: "Order.HasDifferentCurrencies", description: "Order lines have different currencies");
        }
    }
}
