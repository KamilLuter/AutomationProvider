using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Application.Common.Interfaces.Persistance
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
