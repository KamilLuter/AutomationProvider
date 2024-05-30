using AutomationProvider.Domain.Common.Errors;
using AutomationProvider.Domain.Common.Models;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutomationProvider.Domain.CustomerAggregate.ValueObjects
{
    public sealed class Email: ValueObject
    {
        public string Value { get; private set; }
        private Email(string value)
        {
            Value = value;
        }

        public static ErrorOr<Email> Create(string value)
        {
            if (!IsValidEmail(value))
            {
                return Errors.Customer.EmailInWrongFormat;
            }

            return new Email(value);
        }

        private static bool IsValidEmail(string email)
        {
            // Regular expression for basic email format validation
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
