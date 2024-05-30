using AutomationProvider.Application.Common.Interfaces.Services;
using AutomationProvider.Infrastructure.Services.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutomationProvider.Infrastructure.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using AutomationProvider.Domain.CustomerAggregate.ValueObjects;

namespace AutomationProvider.Infrastructure.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly SmtpConfiguration _smtpConfiguration;
        private readonly ILogger<SmtpEmailService> _logger;

        public SmtpEmailService(IOptions<SmtpConfiguration> smtpConfiguration, ILogger<SmtpEmailService> logger)
        {
            _smtpConfiguration = smtpConfiguration.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string emailAddress, string subject, string message)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(_smtpConfiguration.FromAddress);
                    mail.To.Add(emailAddress);
                    mail.Subject = subject;
                    mail.Body = message;
                    mail.IsBodyHtml = false;

                    using (SmtpClient smtp = new SmtpClient(_smtpConfiguration.Host, _smtpConfiguration.Port))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(_smtpConfiguration.Username, _smtpConfiguration.Password);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }   
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            finally
            {
                await Task.CompletedTask;
            }
        }
    }
}
