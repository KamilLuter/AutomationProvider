using AutomationProvider.Domain.Common.Enums;
using AutomationProvider.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomationProvider.Infrastructure.Persistance.Configurations.Extensions
{
    public static class MoneyOwnedEntityConfiguration
    {
        public static OwnedNavigationBuilder<TEntity, Money> ConfigurePrice<TEntity>(
            this OwnedNavigationBuilder<TEntity, Money> builder)
            where TEntity : class
        {
            // Configure the Value property
            builder.Property(m => m.Value)
                .HasColumnName("PriceValue")
                .HasColumnType("decimal(18,2)")
                .IsRequired(); // Optionally specify other configurations for Value property

            // Configure the Currency property
            builder.Property(m => m.Currency)
                .HasColumnName("PriceCurrency")
                .HasColumnType("nvarchar(3)")
                .HasConversion(
                    v => v.ToString(),
                    v => (Currency)Enum.Parse(typeof(Currency), v));

            return builder;
        }
    }
}
