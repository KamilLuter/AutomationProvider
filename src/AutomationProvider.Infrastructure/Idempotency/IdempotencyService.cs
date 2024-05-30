using AutomationProvider.Application.Common.Interfaces.Services;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Memory;

namespace AutomationProvider.Infrastructure.Idempotency
{
    public class IdempotencyService : IIdempotencyService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions _cacheOptions;
        public IdempotencyService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;

            _cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)                                                                           
            };
        }
        public Task CreateRequestAsync(Guid requestId, string name)
        {
            _memoryCache.Set(requestId.ToString(), true, _cacheOptions);
            return Task.CompletedTask;
        }

        public async Task<bool> RequestExsistsAsync(Guid requestId)
        {
            return await Task.FromResult(_memoryCache.TryGetValue(requestId.ToString(), out _));
        }
    }
}
