using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Application.Common.Interfaces.Services
{
    public interface IIdempotencyService
    {
        Task<bool> RequestExsistsAsync(Guid requestId);
        Task CreateRequestAsync(Guid requestId, string name);
    }
}
