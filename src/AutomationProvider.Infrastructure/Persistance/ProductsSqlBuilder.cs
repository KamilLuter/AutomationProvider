using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Infrastructure.Persistance
{
    public class ProductsSqlBuilder
    {
        private readonly StringBuilder _sqlBuilder;

        public ProductsSqlBuilder()
        {
            _sqlBuilder = new StringBuilder("SELECT * FROM Products WHERE 1=1");
        }

        public ProductsSqlBuilder AppendQueryCondition(string key, string value)
        {
            if (IsPriceRangeQuery(key))
            {
                AppendPriceRangeQuery(key, value);
            }
            else if (key.Equals("CatalogId", StringComparison.OrdinalIgnoreCase))
            {
                AppendCatalogIdQuery(value);
            }
            else if (key.Equals("Manufacturer", StringComparison.OrdinalIgnoreCase))
            {
                AppendManufacturerQuery(value);
            }
            else
            {
                AppendAttributeQuery(key, value);
            }

            return this;
        }

        private bool IsPriceRangeQuery(string key)
        {
            return key.Contains("Price");
        }

        private void AppendPriceRangeQuery(string key, string value)
        {
            if (key.Contains("min_") || key.Contains("max_"))
            {
                string comparisonOperator = key.StartsWith("min_") ? ">=" : "<=";
                _sqlBuilder.Append($" AND PriceValue {comparisonOperator} {value}");

            }
            else
            {
                _sqlBuilder.Append($" AND PriceValue = {value}");
            }
        }

        private void AppendCatalogIdQuery(string value)
        {
            _sqlBuilder.Append($" AND CatalogId = '{value}'");
        }

        private void AppendManufacturerQuery(string value)
        {
            _sqlBuilder.Append($" AND Manufacturer = '{value}'");
        }

        private void AppendAttributeQuery(string key, string value)
        {
            string propertyName = key.Contains("min_") || key.Contains("max_") ? key.Substring(4) : key;
            string jsonQuery = key.Contains("min_") || key.Contains("max_")
                ? $"JSON_VALUE(productAttributes, '$.{propertyName}')"
                : $"JSON_VALUE(productAttributes, '$.{key}')";

            _sqlBuilder.Append($" AND {jsonQuery} LIKE '%{value}%'");
        }

        public string Build()
        {
            return _sqlBuilder.ToString();
        }
    }
}
