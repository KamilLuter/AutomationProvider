using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Application.Common.Interfaces.Authentication
{
    public interface IPasswordHasher
    {
        Task<string> HashPasswordAsync(string password);
        Task<bool> VerifyPasswordAsync(string hashedPassword, string providedPassword);
    }
}
