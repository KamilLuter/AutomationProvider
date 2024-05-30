using AutomationProvider.Application.Common.Interfaces.Services;
using AutomationProvider.Domain.Common.Models;
using AutomationProvider.Infrastructure.Migrations;
using AutomationProvider.Infrastructure.Outbox;
using AutomationProvider.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Infrastructure.BackgroundJobs
{
    public class ProcessOutboxMessageJob : IJob
    {
        private readonly AutomationProviderDbContext _dbContext;
        private readonly IPublisher _publisher;
        private readonly IDateTimeProvider _dateTimeProvider;
        public ProcessOutboxMessageJob(
            AutomationProviderDbContext dbContext,
            IPublisher publisher,
            IDateTimeProvider dateTimeProvider)
        {
            _dbContext = dbContext;
            _publisher = publisher;
            _dateTimeProvider = dateTimeProvider;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var messages = await _dbContext
                    .Set<OutboxMessage>()
                    .Where(m => m.ProcessedAtUtc == null)
                    .Take(20)
                    .ToListAsync(context.CancellationToken);

                foreach (OutboxMessage outboxMessage in messages)
                {
                    try
                    {
                        var domainEvent = JsonConvert
                            .DeserializeObject<IDomainEvent>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });

                        if (domainEvent is null)
                        {
                            continue;
                        }

                        await _publisher.Publish(domainEvent, context.CancellationToken);

                        outboxMessage.ProcessedAtUtc = _dateTimeProvider.UtcNow;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred while processing outbox message: {ex.Message}");
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving outbox messages: {ex.Message}");
            }
        }
    }
}
