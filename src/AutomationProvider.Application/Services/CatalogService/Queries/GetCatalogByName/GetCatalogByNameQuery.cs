using AutomationProvider.Domain.CatalogAggregate;
using ErrorOr;
using MediatR;

namespace AutomationProvider.Application.Services.CatalogService.Queries.GetCatalogByName
{
    public record GetCatalogByNameQuery(string name) : IRequest<ErrorOr<Catalog?>>;
}
