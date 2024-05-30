using AutomationProvider.Application.Services.AuthenticationService.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Application.Services.AuthenticationService.Queries.Login
{
    public record LoginQuery(
        string Email
        , string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}
