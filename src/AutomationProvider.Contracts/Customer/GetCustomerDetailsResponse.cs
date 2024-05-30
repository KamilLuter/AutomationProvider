using AutomationProvider.Contracts.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Contracts.User
{
    public record GetCustomerDetailsResponse(
        string FirstName
        , string LastName
        , AddressContract Address
        , PaymentContract prefferedPayment
        , string Email);
}
