using AutomationProvider.Application.Common.Interfaces.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly PasswordHasher<IdentityUser> _passwordHasher;

        public PasswordHasher()
        {
            _passwordHasher = new PasswordHasher<IdentityUser>();
        }

        public async Task<string> HashPasswordAsync(string password)
        {
            return await Task.FromResult(_passwordHasher.HashPassword(null, password));
        }

        public async Task<bool> VerifyPasswordAsync(string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return await Task.FromResult(result == PasswordVerificationResult.Success);
        }
    }
}
