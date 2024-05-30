using AutomationProvider.Domain.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AutomationProvider.Infrastructure.Persistance.Configurations.Extensions;
using AutomationProvider.Domain.CatalogAggregate;
using System.Text.Json;


namespace AutomationProvider.Infrastructure.Persistance.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);

            builder.Property(p => p.Description).HasMaxLength(500);

            builder.Property(p => p.Manufacturer).IsRequired(false).HasMaxLength(100);

            builder.OwnsOne(
                p => p.Price,
                price => price.ConfigurePrice()
            );

            builder.Property(p => p.ProductAttributes)
                .HasConversion(
                    r => JsonSerializer
                        .Serialize(r, JsonSerializerOptions.Default),
                    s => JsonSerializer
                        .Deserialize<Dictionary<string, object>>(
                            s, 
                            JsonSerializerOptions.Default)
                        );

            builder.HasOne<Catalog>()
                .WithMany()
                .HasForeignKey(p => p.CatalogId);
        }
    }
}
