using AutomationProvider.Domain.CatalogAggregate;

namespace AutomationProvider.Application.Common.Interfaces.Persistance
{
    public interface ICatalogRepository
    {
        Task<Catalog?> GetCatalogByIdAsync(Guid catalog, CancellationToken cancellationToken = default);
        Task<Catalog> CreateCatalogAsync(Catalog catalog, CancellationToken cancellationToken = default);
        Task DeleteCatalog(Catalog catalogId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Catalog>> GetAll(CancellationToken cancellationToken = default);
        Task<Catalog?> GetCatalogByNameAsync(string name, CancellationToken cancellationToken = default);
        Catalog? GetCatalogByName(string name);
        Task<string?> GetCatalogNameByIdAsync(Guid catalogId, CancellationToken cancellationToken = default);
    }
}
