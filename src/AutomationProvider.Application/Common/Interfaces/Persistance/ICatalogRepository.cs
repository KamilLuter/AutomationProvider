using AutomationProvider.Domain.CatalogAggregate;

namespace AutomationProvider.Application.Common.Interfaces.Persistance
{
    public interface ICatalogRepository
    {
        Task<Catalog?> GetCatalogByIdAsync(Guid catalog, CancellationToken cancellationToken);
        Task<Catalog> CreateCatalogAsync(Catalog catalog, CancellationToken cancellationToken);
        Task DeleteCatalog(Catalog catalogId, CancellationToken cancellationToken);
        Task<IEnumerable<Catalog>> GetAll(CancellationToken cancellationToken);
        Task<Catalog?> GetCatalogByNameAsync(string name, CancellationToken cancellationToken);
        Catalog? GetCatalogByName(string name);
        Task<string?> GetCatalogNameByIdAsync(Guid catalogId, CancellationToken cancellationToken);
    }
}
