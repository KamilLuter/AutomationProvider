using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Infrastructure.Services.Email
{
    public class SmtpConfiguration
    {
        public const string SectionName = "StmpSettings";
        public string Host { get; init; } = null!;
        public int Port { get; init; }
        public string Username { get; init; } = null!;
        public string Password { get; init; } = null!;
        public bool EnableSsl { get; init; }
        public string FromAddress { get; init; } = null!;
    }
}
