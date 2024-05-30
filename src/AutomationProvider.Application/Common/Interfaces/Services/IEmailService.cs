namespace AutomationProvider.Application.Common.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string emailAddress, string subject, string message);
    }
}
