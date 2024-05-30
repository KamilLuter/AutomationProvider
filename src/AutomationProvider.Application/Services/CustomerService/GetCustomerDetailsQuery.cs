using AutomationProvider.Domain.CustomerAggregate;
using AutomationProvider.Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Application.Services.CustomerService
{
    public record GetCustomerDetailsQuery(
        Guid CustomerId): IRequest<ErrorOr<Customer?>>;
}
