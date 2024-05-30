using AutomationProvider.Domain.CustomerAggregate.ValueObjects;
using AutomationProvider.Infrastructure.Authentication;

namespace AutomationProvider.Application.Common.Interfaces.Authentication
{
    public interface IUserManager
    {
        public Task<User?> FindByEmailAsync(Email email, CancellationToken cancellationToken);
        public Task<bool> CreateAsync(User user, CancellationToken cancellationToken);
        public Task<bool> ValidatePasswordAsync(string password, CancellationToken cancellationToken);
    }
}
