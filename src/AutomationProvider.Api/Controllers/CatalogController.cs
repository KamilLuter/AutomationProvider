using AutomationProvider.Application.Services.CatalogService.Queries.GetCatalogByName;
using AutomationProvider.Application.Services.CatalogService.Queries.GetSubCatalogs;
using AutomationProvider.Contracts.Catalog;
using AutomationProvider.Contracts.Products;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace AutomationProvider.Api.Controllers
{

    [Route("catalogs")]
    public class CatalogController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public CatalogController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<IActionResult> GetCatalogs(CancellationToken cancellationToken)
        {
            GetCatalogsQuery query = new GetCatalogsQuery();
            var result = await _mediator.Send(query, cancellationToken);

            return result.Match(
                result => Ok(_mapper.Map<List<CatalogResponse>>(result)),
                errors => Problem(errors));
        }

        [HttpGet]
        [Route("{catalogName}")]
        public async Task<IActionResult> GetCatalogByName(
            [FromRoute] string catalogName
            , CancellationToken cancellationToken)
        {
            var query = new GetCatalogByNameQuery(catalogName);
            var result = await _mediator.Send(query, cancellationToken);

            return result.Match(
                result => Ok(_mapper.Map<CatalogDetailsResponse>(result)),
                errors => Problem(errors));
        }
    }
}
