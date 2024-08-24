using AutomationProvider.Domain.OrderAggregate;
using AutomationProvider.Domain.OrderAggregate.Entities;
using AutomationProvider.Domain.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AutomationProvider.Infrastructure.Persistance.Configurations.Extensions;

namespace AutomationProvider.Infrastructure.Persistance.Configurations
{
    public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.ToTable("OrderLines");

            builder.HasKey(ol => ol.Id);

            builder.HasOne<Order>()
                .WithMany(o => o.OrderLines)
                .HasForeignKey("OrderLine")
                .IsRequired();

            builder.HasOne<Product>()
                .WithMany() 
                .HasForeignKey(ol => ol.ProductId)
                .IsRequired();

            builder.Property(ol => ol.Quantity)
                .IsRequired();

            builder.OwnsOne(
                ol => ol.Price,
                price => price.ConfigurePrice()
            );
        }
    }
}
