using AutomationProvider.Domain.CatalogAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using AutomationProvider.Domain.CatalogAggregate.ValueObjects;

namespace AutomationProvider.Infrastructure.Persistance.Configurations
{
    public class CatalogConfiguration : IEntityTypeConfiguration<Catalog>
    {
        public void Configure(EntityTypeBuilder<Catalog> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(500);

            //builder.HasMany(c => c.SubCatalogs)
            //       .WithOne()
            //       .HasForeignKey(c => c.Parent)
            //       .IsRequired(false)
            //       .OnDelete(DeleteBehavior.Restrict);

             builder.HasMany(c => c.AvailableProductAttributes) 
               .WithOne()
               .IsRequired(false)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
