

using AutomationProvider.Domain.CustomerAggregate;
using AutomationProvider.Domain.CustomerAggregate.ValueObjects;

namespace AutomationProvider.Application.Common.Interfaces.Persistance
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomerByEmailAsync(Email email, CancellationToken cancellationToken);
        Task<Customer?> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken);
        Task CreateAsync(Customer user, CancellationToken cancellationToken);
        Task<Customer> UpdateDetailsAsync(Customer customer, CancellationToken cancellationToken);
        Task<bool> IsEmailUsed(Email email, CancellationToken cancellationToken);
    }   
}
