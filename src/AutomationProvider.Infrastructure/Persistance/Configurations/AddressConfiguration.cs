using AutomationProvider.Domain.CatalogAggregate;
using AutomationProvider.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProvider.Infrastructure.Persistance.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses"); 

            builder.Property<Guid>("Id") 
                .ValueGeneratedOnAdd();

            builder.HasKey("Id");

            builder.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.StreetNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.ZipCode)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(56);
        }
    }
}
