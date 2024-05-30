using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Domain.CatalogAggregate;
using AutomationProvider.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace AutomationProvider.Application.Services.CatalogService.Queries.GetCatalogByName
{
    public class GetCatalogByNameQueryHandler : IRequestHandler<GetCatalogByNameQuery, ErrorOr<Catalog?>>
    {
        private readonly ICatalogRepository _catalogRepository;
        public GetCatalogByNameQueryHandler(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }
        public async Task<ErrorOr<Catalog?>> Handle(GetCatalogByNameQuery request, CancellationToken cancellationToken)
        {
            var response = await _catalogRepository.GetCatalogByNameAsync(request.name, cancellationToken);

            if (response is null)
            {
                return Errors.Catalog.NotFound;
            }

            return response;
        }
    }
}
