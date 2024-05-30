using AutomationProvider.Domain.Common.Enums;
using AutomationProvider.Domain.Common.Models;
using ErrorOr;

namespace AutomationProvider.Domain.Common.ValueObjects
{
    public sealed class Money : ValueObject
    {
        private Money() 
        { 
        }
        private Money(decimal value, Currency currency)
        {
            Value = value;
            Currency= currency;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Currency;
        }

        public static ErrorOr<Money> Create(decimal value, Currency currency)
        {
            return new Money(value, currency);
        }

        public static ErrorOr<Money> Create(decimal value, string currencyCode)
        {
            if (!Enum.TryParse<Currency>(currencyCode, out Currency currency))
            {
                return Errors.Errors.Common.UnsupportedCurrency;
            }

            return new Money(value, currency);
        }


        public decimal Value { get; private set; }
        public Currency Currency { get; private set; }
    }
}
