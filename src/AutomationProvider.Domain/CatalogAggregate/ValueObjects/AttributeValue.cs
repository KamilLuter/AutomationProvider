using AutomationProvider.Domain.Common.Models;

namespace AutomationProvider.Domain.CatalogAggregate.ValueObjects
{
    public sealed class AttributeValue : ValueObject
    {
        public string Value { get; private set; }
        private AttributeValue(string value)
        {
            Value = value;          
        }

        public static AttributeValue Create(string value)
        {
            return new AttributeValue(value);
        }

#pragma warning disable CS8618
        protected AttributeValue()
        {
        }
#pragma warning restore CS8618
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
