using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace CryptoTax.Data.Context
{
    [Obsolete("Should only be used for generating EF migrations.", true)]
    public class DesignTimeCryptoTaxContextFactory : IDesignTimeDbContextFactory<CryptoTaxContext>
    {
        public CryptoTaxContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CryptoTaxContext>();
            optionsBuilder.UseSqlite($"Data Source={nameof(CryptoTaxContext)}.db");

            return new CryptoTaxContext(optionsBuilder.Options);
        }
    }
}
