using AutomationProvider.Application.Common.Interfaces.Authentication;
using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Application.Services.AuthenticationService.Common;
using AutomationProvider.Domain.Common.Errors;
using AutomationProvider.Domain.CustomerAggregate;
using AutomationProvider.Domain.CustomerAggregate.ValueObjects;
using AutomationProvider.Infrastructure.Authentication;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutomationProvider.Application.Services.AuthenticationService.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPasswordValidator<User> _passwordValidator;
        private readonly IUserManager _userManager;

        public RegisterCommandHandler(
            IJwtTokenGenerator jwtTokenGenerator,
            ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,
            IPasswordValidator<User> passwordValidator,
            IUserManager userManager)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;
            _userManager = userManager;
        }
        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var email = Email.Create(request.Email);

            if (email.IsError)
                return email.Errors;

            if (await _customerRepository.GetCustomerByEmailAsync(email.Value, cancellationToken) is not null)
                return Errors.Customer.EmailAlreadyUsed;

            if (request.Password != request.PasswordConfirmation)
                return Errors.Customer.WrongConfirmationPassword;

            var user = new User();

            var validationResult = await _userManager.ValidatePasswordAsync(request.Password, cancellationToken);
               // = await _passwordValidator.ValidateAsync(_userManager, user, request.Password);

            if (!validationResult)
                return Errors.Customer.PasswordRequirementsNotMet;

            var customer = Customer.Create(
                request.FirstName,
                request.LastName,
                email.Value,
                null,
                null);

            if (customer.IsError)
                return customer.Errors;

            var hashedPassword = await _passwordHasher.HashPasswordAsync(request.Password);

            user.PasswordHash = hashedPassword;
            user.UserName = request.FirstName + '_' + request.LastName;
            user.Email = email.Value.Value;
            user.Id = customer.Value.Id.ToString();

            var registrationResult = await _userManager.CreateAsync(user, cancellationToken);

            if (!registrationResult)
                return Errors.Customer.RegistrationFailed;

            await _customerRepository.CreateAsync(customer.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            var token = _jwtTokenGenerator.GenerateToken(
                customer.Value.Id,
                customer.Value.FirstName,
                customer.Value.LastName);

            return new AuthenticationResult(
                customer.Value,
                token);
        }
    }
}