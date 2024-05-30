using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Domain.CustomerAggregate;
using AutomationProvider.Domain.CustomerAggregate.ValueObjects;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace AutomationProvider.Infrastructure.Persistance.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DbSet<Customer> _customers;

        public CustomerRepository(AutomationProviderDbContext dbContext)
        {
            _customers = dbContext.Customers;
        }

        public async Task CreateAsync(Customer customer, CancellationToken cancellationToken)
        {
            await _customers.AddAsync(customer, cancellationToken);
        }

        public async Task<Customer?> GetCustomerByEmailAsync(Email email, CancellationToken cancellationToken)
        {
            var customer = await _customers
                .Where(c => c.Email == email)
                .FirstOrDefaultAsync(cancellationToken);

            return customer;
        }

        public async Task<Customer?> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var customer = await _customers
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync(cancellationToken);

            return customer;
        }

        public async Task<bool> IsEmailUsed(Email email, CancellationToken cancellationToken)
        {
            var isEmailUsed = await _customers.AnyAsync(c => c.Email == email, cancellationToken);
            return isEmailUsed;
        }

        public async Task<Customer> UpdateDetailsAsync(Customer customer, CancellationToken cancellationToken)
        {
            var updatedCustomerEntry = _customers.Update(customer);
            var updatedCustomer = updatedCustomerEntry.Entity;
            return await Task.FromResult(updatedCustomer);
        }
    }
}
