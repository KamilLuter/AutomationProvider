using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Application.Idempotency
{
    public abstract record IdempotentCommand<T>(Guid RequestId): IRequest<T>;
}
