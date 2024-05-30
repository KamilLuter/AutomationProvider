using AutomationProvider.Application.Common.Interfaces.Authentication;
using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Application.Services.AuthenticationService.Common;
using AutomationProvider.Domain.Common.Errors;
using AutomationProvider.Domain.CustomerAggregate.ValueObjects;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Application.Services.AuthenticationService.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserManager _userManager;
        public LoginQueryHandler(
            IJwtTokenGenerator jwtTokenGenerator
            , ICustomerRepository customerRepository
            , IPasswordHasher passwordHasher
            , IUserManager userManager)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _customerRepository = customerRepository;
            _passwordHasher = passwordHasher;
            _userManager = userManager;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var email = Email.Create(request.Email);
            if (email.IsError)
                return email.Errors;

            var user = await _userManager.FindByEmailAsync(email.Value, cancellationToken);

            if (user is not null)
                return Errors.Customer.WrongPasswordOrEmail;

            if (!(await _passwordHasher.VerifyPasswordAsync(user.PasswordHash, request.Password)))
                return Errors.Customer.WrongPasswordOrEmail;

            var customer = await _customerRepository.GetCustomerByEmailAsync(email.Value, cancellationToken);

            if (customer is null)
                return Errors.Customer.WrongPasswordOrEmail;

            var token = _jwtTokenGenerator.GenerateToken(
                customer.Id,
                customer.FirstName,
                customer.LastName);

            var result = new AuthenticationResult(
                customer,
                token);

            return result;
        }
    }
}
