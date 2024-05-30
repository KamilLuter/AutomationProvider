using AutomationProvider.Contracts.User;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AutomationProvider.Application.Services.CustomerService;
using ErrorOr;
using AutomationProvider.Contracts.Catalog;
using AutomationProvider.Application.Common.Interfaces.Services;
using AutomationProvider.Api.Common.Utils;
using System.Threading;

namespace AutomationProvider.Api.Controllers
{
    [Authorize]
    [Route("User")] 
    public class CustomerController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public CustomerController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("Details")] 
        public async Task<IActionResult> GetUserDetails(CancellationToken cancellationToken)
        {
            var customerId = ClaimsUtils.GetCustomerId(HttpContext);

            if (customerId.IsError)
                return Problem(customerId.Errors);

            var request = new GetCustomerDetailsQuery(customerId.Value);

            var result = await _mediator.Send(request, cancellationToken);

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpPatch("UpdateProfile")]
        public async Task<IActionResult> UpdateUserProfile(
            [FromBody] UpdateCustomerProfileRequest updateUserProfileRequest
            , CancellationToken cancellationToken)
        {
            var customerId = ClaimsUtils.GetCustomerId(HttpContext);

            if (customerId.IsError)
                return Problem(customerId.Errors);

            var request = _mapper.Map<UpdateCustomerDetailsCommand>(
                    (updateUserProfileRequest, customerId.Value));

            var result = await _mediator.Send(request, cancellationToken);

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }
    }
}
