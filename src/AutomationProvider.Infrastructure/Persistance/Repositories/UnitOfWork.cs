using AutomationProvider.Application.Common.Interfaces.Persistance;

namespace AutomationProvider.Infrastructure.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AutomationProviderDbContext _context;
        public UnitOfWork(AutomationProviderDbContext context)
        {
            _context = context;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
