using AutomationProvider.Domain.CatalogAggregate;
using AutomationProvider.Domain.CatalogAggregate.ValueObjects;

namespace AutomationProvider.Application.Services.ProductService.Queries.GetProducts
{
    public static class GetProductsQueryValidator
    {
        public static bool ValidateQuery(
            this Dictionary<string, string> query,
            IReadOnlyList<Attributes> availableProductAttributes)
        {
            return query.AsParallel().All(param => param.ValidateQueryParameter(availableProductAttributes));
        }
        public static bool ValidateQueryParameter(
            this KeyValuePair<string, string> parameter,
            IReadOnlyList<Attributes> availableProductAttributes)
        {
            var key = parameter.Key;
            var value = parameter.Value;

            return ValidateCatalog(key) ||
                   ValidateNumericParameter(key, value) ||
                   ValidateAttributeValue(key, value, availableProductAttributes) ||
                   ValidatePrice(key, value) ||
                   ValidateManufacturer(key);
        }

        public static bool HasNoParameters(
            this Dictionary<string, string> query)
        {
            return query.Count() == 0;
        }

        public static bool HasCatalogParameter(
            this Dictionary<string, string> query)
        {
            return query.Any(att => att.Key == nameof(Catalog));
        }

        private static bool ValidateCatalog(string key)
        {
            return key == "Catalog";
        }

        private static bool ValidateNumericParameter(string key, string value)
        {
            return key.StartsWith("min_") || key.StartsWith("max_") && double.TryParse(value, out _);
        }

        private static bool ValidateAttributeValue(
            string key,
            string value,
            IReadOnlyList<Attributes> availableProductAttributes)
        {
            var catalogAttribute = availableProductAttributes
                                    .FirstOrDefault(attr => attr.Type.ToString().Equals(key));

            if (catalogAttribute is null)
                return false;

            return catalogAttribute.IsRangeAttribute ?
                double.TryParse(value, out _) 
                : catalogAttribute.Values.Any(v => v.Value == value);
        }

        private static bool ValidatePrice(
            string key,
            string value)
        {
            return key == "Price" ? 
                double.TryParse(value, out _) : false;
        }

        private static bool ValidateManufacturer(
            string key)
        {
            return key == "Manufacturer";
        }
    }
}
