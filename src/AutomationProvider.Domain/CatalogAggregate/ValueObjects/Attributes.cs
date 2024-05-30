using AutomationProvider.Domain.Common.Enums;
using AutomationProvider.Domain.Common.Models;
using AutomationProvider.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Domain.CatalogAggregate.ValueObjects
{
    public sealed class Attributes: ValueObject
    {
        private readonly List<AttributeValue> _values = new List<AttributeValue>();
        public AttributeType Type { get; private set; }
        public string Name { get; private set; }
        public string? Unit { get; private set; }
        public  bool IsRangeAttribute { get; private set; }
        public IReadOnlyList<AttributeValue> Values => _values.AsReadOnly();

        private Attributes(string name, string unit, AttributeType type, bool isRangeAttribute, List<AttributeValue> values)
        {
            Name = name;
            Type = type;
            IsRangeAttribute = isRangeAttribute;
            _values = values;
            Unit = unit;
        }
        public void AddValue(AttributeValue value)
        {
            if (value != null &&  !_values.Contains(value))
                _values.Add(value);
        }

        public static Attributes Create(string name, string unit, AttributeType type, bool isRangeAttribute, List<AttributeValue> values)
        {
            return new Attributes(name, unit, type, isRangeAttribute, values);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Values;
            yield return Type;
            yield return IsRangeAttribute;
            yield return Name;
            yield return Unit;
        }
#pragma warning disable CS8618
        protected Attributes()
        {
        }
#pragma warning restore CS8618
    }
}
