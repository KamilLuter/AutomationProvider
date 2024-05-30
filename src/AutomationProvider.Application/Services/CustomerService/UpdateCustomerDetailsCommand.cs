using AutomationProvider.Contracts.Orders;
using AutomationProvider.Domain.CustomerAggregate;
using ErrorOr;
using MediatR;

namespace AutomationProvider.Application.Services.CustomerService
{
    public record UpdateCustomerDetailsCommand(
        Guid CustomerId
        , string FirstName
        , string LastName
        , AddressContract Address
        , PaymentContract Payment) : IRequest<ErrorOr<Customer>>;
}
