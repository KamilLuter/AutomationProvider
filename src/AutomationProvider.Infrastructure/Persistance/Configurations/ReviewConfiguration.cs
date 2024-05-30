using AutomationProvider.Domain.CustomerAggregate;
using AutomationProvider.Domain.Product;
using AutomationProvider.Domain.Ratings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomationProvider.Infrastructure.Persistance.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                .ValueGeneratedNever();

            builder.Property(r => r.CustomerName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Comment)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(r => r.CustomerId)
                .IsRequired(false);

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(r => r.ProductId)
                .IsRequired();

            builder.Property(r => r.Rating)
                .IsRequired(false);
        }
    }
}
