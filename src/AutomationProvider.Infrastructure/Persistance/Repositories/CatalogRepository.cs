using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Domain.CatalogAggregate;
using AutomationProvider.Domain.CatalogAggregate.ValueObjects;
using AutomationProvider.Domain.Common.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationProvider.Infrastructure.Persistance.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly DbSet<Catalog> _catalogs;
        public CatalogRepository(AutomationProviderDbContext dbContext)
        {
            _catalogs = dbContext.Catalogs;
        }

        private async Task PopulateSubCatalogs(Catalog catalog, CancellationToken cancellationToken)
        {
            await _catalogs.Entry(catalog)
                .Collection(c => c.SubCatalogs)
                .LoadAsync(cancellationToken);

            foreach (var subCatalog in catalog.SubCatalogs)
            {
                await PopulateSubCatalogs(subCatalog, cancellationToken); 
            }
        }
        public async Task<Catalog> CreateCatalogAsync(Catalog catalog, CancellationToken cancellationToken)
        {
            await _catalogs.AddAsync(catalog, cancellationToken);
            return catalog;
        }

        public async Task DeleteCatalog(Catalog catalog, CancellationToken cancellationToken)
        {
            _catalogs.Remove(catalog);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Catalog>> GetAll(CancellationToken cancellationToken)
        {
            return await _catalogs
                .Include(c => c.AvailableProductAttributes)
                .ToListAsync(cancellationToken);
        }

        public async Task<Catalog?> GetCatalogByIdAsync(Guid catalogId, CancellationToken cancellationToken)
        {
            return await _catalogs.Include(c => c.AvailableProductAttributes)
                .FirstOrDefaultAsync(c => c.Id == catalogId, cancellationToken);
        }
        public async Task<Catalog?> GetCatalogByNameAsync(string name, CancellationToken cancellationToken)
        {
            var result = await _catalogs.Include(c => c.AvailableProductAttributes)
                .FirstOrDefaultAsync(c => c.Name == name, cancellationToken);
            return result;
        }

        public async Task<string?> GetCatalogNameByIdAsync(Guid catalogId, CancellationToken cancellationToken)
        {
            var catalogName = await _catalogs
                .Where(c => c.Id == catalogId)
                .Select(c => c.Name)
                .FirstOrDefaultAsync(cancellationToken);
            return catalogName;
        }

        public Catalog? GetCatalogByName(string name)
        {
            var result = _catalogs.Include(c => c.AvailableProductAttributes)
                .FirstOrDefault(c => c.Name == name);
            return result;
        }
    }
}
