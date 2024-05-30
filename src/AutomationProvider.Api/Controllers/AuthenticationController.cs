using AutomationProvider.Application.Services.AuthenticationService.Commands.Register;
using AutomationProvider.Application.Services.AuthenticationService.Queries.Login;
using AutomationProvider.Contracts.Authentication;
using AutomationProvider.Contracts.Catalog;
using AutomationProvider.Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutomationProvider.Api.Controllers
{
    [Route("auth")]
    [AllowAnonymous]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public AuthenticationController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;  
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterRequest request,
            [FromHeader(Name = "X-Idempotency-Key")] string requestId,
            CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(requestId, out Guid requestGuidId))
                return Problem(Errors.Common.InvalidRequest.ToString());

            var command = _mapper.Map<RegisterCommand>((request, requestId));
            var result = await _mediator.Send(command, cancellationToken);

            return result.Match(
                result => Ok(_mapper.Map<AuthenticationResponse>(result)),
                errors => Problem(errors));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequest request
            , CancellationToken cancellationToken)
        {
            var query = _mapper.Map<LoginQuery>(request);
            var result = await _mediator.Send(query, cancellationToken);

            return result.Match(
                result => Ok(_mapper.Map<AuthenticationResponse>(result)),
                errors => Problem(errors));
        }
    }
}
