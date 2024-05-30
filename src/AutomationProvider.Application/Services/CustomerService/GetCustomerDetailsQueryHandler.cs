using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Domain.CustomerAggregate;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Application.Services.CustomerService
{
    public class GetCustomerDetailsQueryHandler : IRequestHandler<GetCustomerDetailsQuery, ErrorOr<Customer?>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerDetailsQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ErrorOr<Customer?>> Handle(GetCustomerDetailsQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(request.CustomerId, cancellationToken);
            return customer;
        }
    }
}
