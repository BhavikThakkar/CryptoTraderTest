using CryptoTax.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoTax.Data.Context
{
    public class CryptoTaxContext : DbContext
    {
        public const string CURRENT_USER_EMAIL = "dev@cryptotrader.tax";

        public CryptoTaxContext(DbContextOptions<CryptoTaxContext> options) : base(options) { }

        // User Tables
        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        // Billing Tables
        public DbSet<BillingTier> BillingTiers { get; set; }

        // Configuration
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(CryptoTaxContext).Assembly);
        }
    }
}
