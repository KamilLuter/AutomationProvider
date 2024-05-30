using AutomationProvider.Application.Idempotency;
using AutomationProvider.Application.Services.AuthenticationService.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Application.Services.AuthenticationService.Commands.Register
{
    public record RegisterCommand(
        Guid RequestId,
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string PasswordConfirmation) : IdempotentCommand<ErrorOr<AuthenticationResult>>(RequestId);
}
