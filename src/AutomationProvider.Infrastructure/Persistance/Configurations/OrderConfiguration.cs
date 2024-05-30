using AutomationProvider.Domain.Common.ValueObjects;
using AutomationProvider.Domain.CustomerAggregate;
using AutomationProvider.Domain.Order;
using AutomationProvider.Domain.OrderAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AutomationProvider.Infrastructure.Persistance.Configurations.Extensions;

namespace AutomationProvider.Infrastructure.Persistance.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.CreatedAt)
                .IsRequired();

            builder.Property(o => o.DeliveredAt)
                .IsRequired(false);

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .IsRequired(false);

            builder.HasMany(o => o.OrderLines)
                .WithOne()
                .HasForeignKey("OrderId");

            builder.HasOne(o => o.ShippingAddress)
                .WithOne()
                .HasForeignKey<Address>("Id");

            builder.OwnsOne(
                p => p.Price,
                price => price.ConfigurePrice());
        }
    }
}
