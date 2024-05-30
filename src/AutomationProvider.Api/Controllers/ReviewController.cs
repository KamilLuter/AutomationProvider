using AutomationProvider.Api.Common.Utils;
using AutomationProvider.Application.Services.ReviewService.Commands;
using AutomationProvider.Application.Services.ReviewService.Queries;
using AutomationProvider.Contracts.Reviews;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace AutomationProvider.Api.Controllers
{
    [Route("review")]
    [AllowAnonymous]
    public class ReviewController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public ReviewController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("productId={productId}/{page}")]
        public async Task<IActionResult> GetProductReviews(
            [FromRoute] Guid productId,
            [FromRoute] int page,
            CancellationToken cancellationToken)
        {
            var query = new GetProductReviewsQuery(productId, page);

            var result = await _mediator.Send(query, cancellationToken);

            return result.Match(
                result => Ok(_mapper.Map<List<GetReviewsResult>>(result)),
                errors => Problem(errors));
        }

        [HttpGet("Id={reviewId}")]
        public async Task<IActionResult> GetReview(
            [FromRoute] Guid reviewId,
            CancellationToken cancellationToken)
        {
            var query = new GetReviewByIdQuery(reviewId);

            var result = await _mediator.Send(query, cancellationToken);

            return result.Match(
                result => Ok(_mapper.Map<GetReviewsResult>(result)),
                errors => Problem(errors));
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddComment(
            [FromBody] AddCommentRequest addCommentRequest,
            CancellationToken cancellationToken)
        {
            var contextCustomerId = ClaimsUtils.GetCustomerId(HttpContext);
            Guid? userId = null;

            if (!contextCustomerId.IsError)
                userId = contextCustomerId.Value;

            var request = _mapper.Map<AddCommentCommand>((addCommentRequest, userId));

            var result = await _mediator.Send(request, cancellationToken);

            return result.Match(
                result => Ok(_mapper.Map<ReviewResult>(result)),
                errors => Problem(errors));
        }

        [HttpGet("delete/reviewId={reviewId}")]
        public async Task<IActionResult> DeleteReview(
            [FromRoute] Guid reviewId,
            CancellationToken cancellationToken)
        {
            var customerId = ClaimsUtils.GetCustomerId(HttpContext);

            if (customerId.IsError)
                return Problem(customerId.Errors);

            var query = new DeleteReviewCommand(reviewId, customerId.Value);

            var result = await _mediator.Send(query, cancellationToken);

            return result is null ? Ok() : Problem(result);
        }
    }
}
