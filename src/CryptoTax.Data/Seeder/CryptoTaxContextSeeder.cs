using CryptoTax.Data.Context;
using CryptoTax.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoTax.Data.Seeder
{
    public static class CryptoTaxContextSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetRequiredService<DbContextOptions<CryptoTaxContext>>();
            using var db = new CryptoTaxContext(options);

            // Apply Migrations
            await db.Database.MigrateAsync();

            // Return If Users Already Exist
            if (db.Users.AsQueryable().Any())
                return;

            // Billing Tiers
            var hobbyist = BillingTier.Create(name: "Hobbyist", threshold: 100, fullAmountInCents: 4999);
            var dayTrader = BillingTier.Create(name: "Day Trader", threshold: 1500, fullAmountInCents: 9999);
            var highVolumeTrader = BillingTier.Create(name: "High Volume Trader", threshold: 5000, fullAmountInCents: 19999);
            var unlimited = BillingTier.Create(name: "Unlimited", threshold: int.MaxValue, fullAmountInCents: 29999);

            db.BillingTiers.Add(hobbyist);
            db.BillingTiers.Add(dayTrader);
            db.BillingTiers.Add(highVolumeTrader);
            db.BillingTiers.Add(unlimited);

            // Users
            var mitchell = User.Create("Mitchell", "mitchell@cryptotrader.tax");
            var mitchellPurchase = Purchase.Create(
                taxYear: 2020,
                timestamp: DateTime.UtcNow,
                amountInCents: 4999,
                billingTier: hobbyist);

            mitchell.Purchases.Add(mitchellPurchase);

            var lucas = User.Create("Lucas", "lucas@cryptotrader.tax");
            var lucasReport = Report.Create(
                taxYear: 2018,
                tradeCount: 8,
                incomingTxCount: 0,
                outgoingTxCount: 0,
                netRealizedGains: 488.87m);

            lucas.Reports.Add(lucasReport);

            var dev = User.Create("Test User", CryptoTaxContext.CURRENT_USER_EMAIL);
            var devReport2018 = Report.Create(
                taxYear: 2018,
                tradeCount: 32,
                incomingTxCount: 500,
                outgoingTxCount: 0,
                netRealizedGains: 48063.58m);

            var devReport2019 = Report.Create(
                taxYear: 2019,
                tradeCount: 4500,
                incomingTxCount: 5,
                outgoingTxCount: 1000,
                netRealizedGains: 632.60m);

            var devReport2020 = Report.Create(
                taxYear: 2020,
                tradeCount: 12000,
                incomingTxCount: 8,
                outgoingTxCount: 58,
                netRealizedGains: 632.60m);

            dev.Reports.Add(devReport2018);
            dev.Reports.Add(devReport2019);
            dev.Reports.Add(devReport2020);

            // Add Users
            db.Users.Add(lucas);
            db.Users.Add(mitchell);
            db.Users.Add(dev);

            await db.SaveChangesAsync();
        }
    }
}
