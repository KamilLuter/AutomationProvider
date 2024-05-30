using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Domain.CatalogAggregate;
using AutomationProvider.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace AutomationProvider.Application.Services.CatalogService.Queries.GetSubCatalogs
{
    public class GetCatalogsTreeQueryHandler : IRequestHandler<GetCatalogsQuery, ErrorOr<IEnumerable<Catalog>?>>
    {
        private readonly ICatalogRepository _catalogRepository;
        public GetCatalogsTreeQueryHandler(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }
        public async Task<ErrorOr<IEnumerable<Catalog>?>> Handle(GetCatalogsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var catalogs = await _catalogRepository.GetAll(cancellationToken);
                return catalogs.ToList();
            }
            catch
            {
                return Errors.Catalog.NotFound;
            }                
        }
    }
}
