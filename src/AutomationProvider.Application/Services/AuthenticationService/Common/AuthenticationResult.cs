using AutomationProvider.Domain.CustomerAggregate;

namespace AutomationProvider.Application.Services.AuthenticationService.Common
{
    public record AuthenticationResult(
        Customer user,
        string Token);
}
