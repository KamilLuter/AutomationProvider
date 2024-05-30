using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Domain.Common.Errors
{
    public partial class Errors
    {
        public static class Customer
        {
            public static Error EmailAlreadyUsed 
                => Error.Conflict(code: "Customer.EmailAlreadyUsed", description: "Email already used");

            public static Error EmailInWrongFormat =>
                Error.Validation(code: "Customer.EmailWrongFormat", description: "Email is in wrong format");

            public static Error PhoneNumberInWrongFormat =>
                Error.Validation(code: "Customer.PhoneNumberInWrongFormat", description: "Phone number is in wrong format");

            public static Error WrongPasswordOrEmail =>
                Error.NotFound(code: "Customer.WrongPasswordOrEmail", description: "Wrong password or email");

            public static Error WrongConfirmationPassword =>
                Error.Validation(code: "Customer.WrongConfirmationPassword", description: "Passwords are not matching");

            public static Error NotFound =>
                Error.NotFound(code: "Customer.NotFound", description: "Customer not found");

            public static Error FirstNameNotProvided =>
                Error.Validation(code: "Customer.FirstNameNotProvided", description: "First name was not provided");

            public static Error LastNameNotProvided =>
                Error.Validation(code: "Customer.LastNameNotProvided", description: "Last name was not provided");

            public static Error PasswordRequirementsNotMet =>
               Error.Validation(code: "Customer.PasswordRequirementsNotMet", description: "Password requirements not met");

            public static Error RegistrationFailed =>
                Error.Failure(code: "Customer.RegistrationFailed", description: "Registration failed");
        }
    }
}
