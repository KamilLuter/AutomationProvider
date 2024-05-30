using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Domain.CustomerAggregate;
using ErrorOr;
using MediatR;
using AutomationProvider.Domain.Common.Errors;

namespace AutomationProvider.Application.Services.CustomerService
{
    public class UpdateCustomerDetailsCommandHandler : IRequestHandler<UpdateCustomerDetailsCommand, ErrorOr<Customer>>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerDetailsCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ErrorOr<Customer>> Handle(UpdateCustomerDetailsCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(request.CustomerId, cancellationToken);

            if (customer is null)
                return Errors.Customer.NotFound;

            var address = customer.UpdateAddress(request.Address.Street,
                request.Address.StreetNumber,
                request.Address.City,
                request.Address.ZipCode,
                request.Address.Country);

            if (address.IsError)
                return address.Errors;

            var lastName = customer.UpdateLastName(request.LastName);

            if (lastName.IsError)
                return lastName.Errors;

            var firstName = customer.UpdateFirstName(request.FirstName);

            if (firstName.IsError)
                return firstName.Errors;


            return customer;
        }
    }
}
