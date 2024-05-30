using AutomationProvider.Domain.Common.Models;
using AutomationProvider.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace AutomationProvider.Infrastructure.Interceptors
{
    public sealed class ConvertDomainEventsToOutboxMessageInterceptor
        : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            DbContext? dbContext = eventData.Context;

            if(dbContext is null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            var outboxMessage = dbContext.ChangeTracker
                .Entries<AggregateRoot<Guid>>()
                .Select(x => x.Entity)
                .SelectMany(agg =>
                {
                    var domainEvents = agg.GetDomainEvents();
                    agg.ClearDomainEvents();
                    return domainEvents;
                })
                .Select(de => new OutboxMessage
                {
                    Id = Guid.NewGuid(),
                    OccuredOnUtc = DateTime.UtcNow,
                    Type = de.GetType().Name,
                    Content = JsonConvert.SerializeObject(
                        de,
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        })
                })
                .ToList();

            dbContext.Set<OutboxMessage>().AddRange(outboxMessage);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
