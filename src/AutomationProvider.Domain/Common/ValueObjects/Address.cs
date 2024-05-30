using AutomationProvider.Domain.Common.Models;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Domain.Common.ValueObjects
{
    public sealed class Address : ValueObject
    {
        public string Street { get; private set; }
        public string StreetNumber { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }
        public string Country { get; private set; }
        private Address()
        {
        }
        private Address(string street, string streetNumber, string city, string zipCode, string country)
        {
            Street = street;
            StreetNumber = streetNumber;
            City = city;
            ZipCode = zipCode;
            Country = country;
        }

        public static ErrorOr<Address> Create(string street, string streetNumber, string city, string zipCode, string country)
        {
            if (string.IsNullOrEmpty(street) 
                || string.IsNullOrEmpty(streetNumber) 
                || string.IsNullOrEmpty(city)
                || string.IsNullOrEmpty(zipCode)
                || string.IsNullOrEmpty(country))
            {
                return Errors.Errors.Common.InvalidAddress;
            }

            return new Address(street, streetNumber, city, zipCode, country);
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return StreetNumber;
            yield return ZipCode;
            yield return Country;
        }
    }
}
