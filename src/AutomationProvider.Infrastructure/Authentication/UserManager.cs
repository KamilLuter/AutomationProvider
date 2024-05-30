using AutomationProvider.Application.Common.Interfaces.Authentication;
using AutomationProvider.Domain.CustomerAggregate.ValueObjects;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Infrastructure.Authentication
{
    public class UserManager : IUserManager
    {
        private readonly UserManager<User> _identityUserManager;
        private readonly IPasswordValidator<User> _passwordValidator;
        public UserManager(UserManager<User> identityUserManager, IPasswordValidator<User> passwordValidator)
        {
            _identityUserManager = identityUserManager;
            _passwordValidator = passwordValidator;
        }

        public async Task<bool> CreateAsync(User user, CancellationToken cancellationToken)
        {
            var result = await _identityUserManager.CreateAsync(user);
            return result.Succeeded;
        }

        public async Task<User?> FindByEmailAsync(Email email, CancellationToken cancellationToken)
        {
            var user = await _identityUserManager.FindByEmailAsync(email.Value);
            return user;
        }

        public async Task<bool> ValidatePasswordAsync(string password, CancellationToken cancellationToken)
        {
            var result = await _passwordValidator.ValidateAsync(_identityUserManager, null, password);
            return result.Succeeded;
        }
    }
}
