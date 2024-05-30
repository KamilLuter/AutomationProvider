using AutomationProvider.Application.Services.ProductService.Commands.CreateProductCommand;
using AutomationProvider.Application.Services.ProductService.Queries.GetProductDetailedData;
using AutomationProvider.Application.Services.ProductService.Queries.GetProducts;
using AutomationProvider.Contracts.Products;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutomationProvider.Domain.Common.Errors;
using AutomationProvider.Application.Services.ProductService.Commands.Common;

namespace AutomationProvider.Api.Controllers
{
    [Route("products")]
    [AllowAnonymous]
    public class ProductController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public ProductController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("Page={page}&PageSize={pageSize}&OrderBy={orderBy}")]
        public async Task<IActionResult> GetProductsByQuery(
            [FromQuery] Dictionary<string, string> parameters,
            [FromRoute] string orderBy,
            [FromRoute] int page,
            [FromRoute] int pageSize,
            CancellationToken cancellationToken)
        {
            var query = new GetProductsQuery(parameters, orderBy, page, pageSize);

            var result = await _mediator.Send(query, cancellationToken);

            return result.Match(
                result => Ok(_mapper.Map<List<GetProductResponse>>(result)),
                errors => Problem(errors));
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductDetails(
            string productId
            , CancellationToken cancellationToken)
        {
            var query = new GetProductByIdQuery(productId);

            var productResult = await _mediator.Send(query, cancellationToken);

            return productResult.Match(
                productResult => Ok(_mapper.Map<GetProductResponse>(productResult)),
                errors => Problem(errors));
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateProduct(
            [FromBody] CreateProductRequest request,
            [FromHeader(Name = "X-Idempotency-Key")] string requestId,
            CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(requestId, out Guid requestGuidId))
                return Problem(Errors.Common.InvalidRequest.ToString());

            var command = _mapper.Map<CreateProductCommand>((request, requestGuidId));

            var result = await _mediator.Send(command);

            return result.Match(
                productResult => Ok(_mapper.Map<GetProductResponse>(result)),
                errors => Problem(errors));
        }

        //[HttpPatch("{productId}")]
        //[Authorize]
        //public async Task<IActionResult> UpdateProduct(
        //    string productId
        //    , [FromBody] UpdateProductRequest createProductRequest
        //    , CancellationToken cancellationToken)
        //{
        //    await Task.CompletedTask;
        //    return Ok();
        //}

    }
}
