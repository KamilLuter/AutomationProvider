using AutomationProvider.Domain.Common.Errors;
using AutomationProvider.Domain.Common.Models;
using AutomationProvider.Domain.Common.ValueObjects;
using AutomationProvider.Domain.CustomerAggregate.Events;
using AutomationProvider.Domain.CustomerAggregate.ValueObjects;
using ErrorOr;

using System.Text.RegularExpressions;

namespace AutomationProvider.Domain.CustomerAggregate
{
    public class Customer : AggregateRoot<Guid>
    {
        private readonly List<Guid> _orders= new List<Guid>();
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Email Email { get; private set; }
        public string? PhoneNumber { get; private set; }
        public Address? Address { get; private set; }
        public IReadOnlyList<Guid> Orders => _orders.AsReadOnly();
        private Customer(Guid userId, string firstName, string lastName, Email email, string? phoneNumber, Address? address) : base(userId)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Email = email;
            PhoneNumber = phoneNumber;
        }
        public static ErrorOr<Customer> Create(string firstName, string lastName, Email email, string? phoneNumber, Address? address)
        {
            if (phoneNumber != null)
                if (IsPhoneNumberValid(phoneNumber))
                    return Errors.Customer.PhoneNumberInWrongFormat;

            var customer = new Customer(Guid.NewGuid(), firstName, lastName, email, phoneNumber, address);

            customer.RaiseDomainEvent(new CustomerCreatedDomaintEvent(customer.Id));

            return customer;
        }

        public ErrorOr<Address> UpdateAddress(
              string street
            , string streetNumber
            , string city
            , string zipCode
            , string country)
        {
            var address = Address.Create(street, streetNumber, city, zipCode, country);

            if (!address.IsError)
            {
                this.Address = address.Value;
            }

            return address;
        }

        public ErrorOr<string> UpdateFirstName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Errors.Customer.FirstNameNotProvided;
            else
            {
                this.FirstName = name;
                return name;
            }               
        }

        public ErrorOr<string> UpdateLastName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Errors.Customer.LastNameNotProvided;
            else
            {
                this.LastName = name;
                return name;
            }
        }

        private static bool IsPhoneNumberValid(string phoneNumber)
        {
            string pattern = @"^\+\d{1,3}\s?\(\d{3}\)\s?\d{3}-\d{4}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(phoneNumber);
        }

        public void AddOrder(Guid orderId)
        {
            _orders.Add(orderId);
        }
        #pragma warning disable CS8618
        protected Customer()
        {
        }
        #pragma warning restore CS8618
    }
}
