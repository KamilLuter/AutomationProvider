using AutomationProvider.Domain.CatalogAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AutomationProvider.Infrastructure.Persistance.Configurations.Extensions;

namespace AutomationProvider.Infrastructure.Persistance.Configurations
{
    public class AttributesConfiguration : IEntityTypeConfiguration<Attributes>
    {
        public void Configure(EntityTypeBuilder<Attributes> builder)
        {
            builder.Property<int>("Id");
            builder.HasKey("Id");

            builder.Property(a => a.Type).IsRequired();
            builder.Property(a => a.IsRangeAttribute).IsRequired();
            builder.OwnsMany(
                a => a.Values,
                b => {
                    b.Property(x => x.Value);
                    b.ToJson();
                });
        }
    }
}
