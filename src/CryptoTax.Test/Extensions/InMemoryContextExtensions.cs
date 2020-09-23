using System;
using CryptoTax.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;

namespace CryptoTax.Test.Extensions
{
    public static class InMemoryContextExtensions
    {
        public static DbContextOptions<CryptoTaxContext> CreateInMemoryOptions(string databaseName = null, InMemoryDatabaseRoot root = null)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CryptoTaxContext>();

            optionsBuilder
                .UseLazyLoadingProxies()
                .ConfigureWarnings(x =>
                {
                    x.Ignore(InMemoryEventId.TransactionIgnoredWarning);
                })
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

            if (root is null)
                optionsBuilder.UseInMemoryDatabase(databaseName ?? Guid.NewGuid().ToString());
            else
                optionsBuilder.UseInMemoryDatabase(databaseName ?? Guid.NewGuid().ToString(), root);

            return optionsBuilder.Options;
        }

        public static CryptoTaxContext CreateTestContext(string databaseName = null, InMemoryDatabaseRoot root = null)
        {
            return new CryptoTaxContext(CreateInMemoryOptions(databaseName, root));
        }
    }
}