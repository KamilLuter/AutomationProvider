using AutomationProvider.Api.Common.Utils;
using AutomationProvider.Application.Common.Interfaces.Services;
using AutomationProvider.Application.Services.OrderService.CreateOrder;
using AutomationProvider.Application.Services.OrderService.GetOrderDetails;
using AutomationProvider.Application.Services.OrderService.GetOrdersByDate;
using AutomationProvider.Contracts.Orders;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutomationProvider.Domain.Common.Errors;

namespace AutomationProvider.Api.Controllers
{
    [Route("Orders")]
    public class OrderController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        private readonly IDateTimeProvider _dateTimeProvider;

        public OrderController(ISender mediator, IMapper mapper, IDateTimeProvider dateTimeProvider)
        {
            _mediator = mediator;
            _mapper = mapper;
            _dateTimeProvider = dateTimeProvider;
        }

        [Authorize]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderDetails([FromRoute] string orderId)
        {
            var customerId = ClaimsUtils.GetCustomerId(HttpContext);

            if (customerId.IsError)
                return Problem(customerId.Errors);

            var request = new GetOrderDetailsQuery(new Guid(orderId), customerId.Value);

            var result = await _mediator.Send(request);

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrders(
            [FromQuery] GetOrdersByDateRequest request
            , CancellationToken cancellationToken)
        {
            var customerId = ClaimsUtils.GetCustomerId(HttpContext);

            if (customerId.IsError)
                return Problem(customerId.Errors);

            var query = _mapper.Map<GetOrdersByDateQuery>((request, customerId.Value, _dateTimeProvider.UtcNow));

            var result = await _mediator.Send(query, cancellationToken);

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));       
        }

        [HttpPost("/Create")]
        public async Task<IActionResult> CreateOrder(
            [FromBody] CreateOrderRequest createOrderRequest,
            [FromHeader(Name = "X-Idempotency-Key")] string requestId,
            CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(requestId, out Guid requestGuidId))
                return Problem(Errors.Common.InvalidRequest.ToString());

            var query = _mapper.Map<CreateOrderCommand>((createOrderRequest, requestGuidId));

            var result = await _mediator.Send(query, cancellationToken);

            return result.Match(
                result => Ok(_mapper.Map<GetOrderDetailsResponse>(result)),
                errors => Problem(errors));
        }
    }
}
