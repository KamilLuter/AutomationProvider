using AutomationProvider.Domain.Common.ValueObjects;
using AutomationProvider.Domain.CustomerAggregate;
using AutomationProvider.Domain.CustomerAggregate.ValueObjects;
using AutomationProvider.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomationProvider.Infrastructure.Persistance.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.LastName).IsRequired().HasMaxLength(50);

            builder.Property(c => c.Email)
                .HasColumnName("Email")
                .HasConversion(
                    email => email.Value, 
                    value => Email.Create(value).Value 
                )
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.HasMany<Order>()
                .WithOne() 
                .HasForeignKey(o => o.CustomerId);

            builder.HasOne(c => c.Address)
                   .WithOne()
                   .HasForeignKey<Customer>("AddressId")
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
