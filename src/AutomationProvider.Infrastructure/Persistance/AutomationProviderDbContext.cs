using AutomationProvider.Domain.CatalogAggregate;
using AutomationProvider.Domain.Common.ValueObjects;
using AutomationProvider.Domain.Order;
using AutomationProvider.Domain.OrderAggregate.Entities;
using AutomationProvider.Domain.Product;
using AutomationProvider.Domain.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using AutomationProvider.Domain.CatalogAggregate.ValueObjects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AutomationProvider.Infrastructure.Authentication;
using AutomationProvider.Infrastructure.Outbox;
using AutomationProvider.Domain.Ratings;

namespace AutomationProvider.Infrastructure.Persistance
{
    public class AutomationProviderDbContext : IdentityDbContext<User>
    {
        public AutomationProviderDbContext(DbContextOptions<AutomationProviderDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products {  get; set; } = null!;
        public DbSet<Catalog> Catalogs { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderLine> OrdersLines { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Address> Addresss { get; set; } = null!;
        public DbSet<Attributes> Attributes { get; set; } = null!;
        public DbSet<OutboxMessage> OutboxMessage { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AutomationProviderDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
