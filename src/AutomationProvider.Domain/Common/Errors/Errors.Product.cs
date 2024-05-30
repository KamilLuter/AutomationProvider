using ErrorOr;

namespace AutomationProvider.Domain.Common.Errors
{
    public partial class Errors
    {
        public static class Product
        {
            public static Error NotFound => Error.NotFound(
                code: "Product.NotFound",
                description: "Product was not found");
            public static Error CategoryNotProvided => Error.Failure(
                code: "Product.CatalogNotProvided", 
                description: "Category was not provided");
            public static Error WrongNumericAttributeValue => Error.Failure(
                code: "Product.WrongNumericAttributeValue", 
                description: "Wrong numeric value in query");
            public static Error WrongEnumAttributeValue => Error.Failure(
                code: "Product.WrongEnumAttributeValue",
                description: "Wrong value for attribute in query");

            public static Error WrongAttributeName => Error.NotFound(
                code: "Product.WrongAttributeName",
                description: "Attribute doesn't exsist");

            public static Error CreateError => Error.Failure(
                code: "Product.CreateError",
                description: "Product was not created");
        }
    }
}
