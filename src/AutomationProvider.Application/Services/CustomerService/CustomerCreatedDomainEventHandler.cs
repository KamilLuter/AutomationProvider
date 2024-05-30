using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Application.Common.Interfaces.Services;
using AutomationProvider.Domain.CustomerAggregate.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Application.Services.CustomerService
{
    public sealed class CustomerCreatedDomainEventHandler
        : INotificationHandler<CustomerCreatedDomaintEvent>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmailService _emailService;
        public CustomerCreatedDomainEventHandler(ICustomerRepository customerRepository, IEmailService emailService)
        {
            _customerRepository = customerRepository;
            _emailService = emailService;
        }
        public async Task Handle(CustomerCreatedDomaintEvent notification, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(notification.CustomerId, cancellationToken);

            if (customer is null)
            {
                return;
            }

            var emailAddress = customer.Email; 
            var subject = "Welcome to our platform!";
            var message = $"Dear {customer.FirstName}, welcome to our platform!"; 

            await _emailService.SendEmailAsync(emailAddress.Value, subject, message);
            return;
        }
    }
}
