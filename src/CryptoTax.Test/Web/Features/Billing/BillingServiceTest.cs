using System;
using System.Threading.Tasks;
using CryptoTax.Data.Context;
using CryptoTax.Data.Entities;
using CryptoTax.Web.Features.Authorization.Services;
using CryptoTax.Web.Features.Billing.Models;
using CryptoTax.Web.Features.Billing.Services;
using FluentAssertions;
using NUnit.Framework;
using static CryptoTax.Test.Extensions.InMemoryContextExtensions;

namespace CryptoTax.Test.Web.Features.BillingServiceTest
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class BillingServiceTest
    {
        #region Setup

        private sealed class Setup : IDisposable
        {
            public Setup(CryptoTaxContext dbContext)
            {
                Db = dbContext;
            }

            public CryptoTaxContext Db;

            public void Dispose()
            {
                Db?.Dispose();
            }
        }

        private Setup Initialize()
        {
            var db = CreateTestContext();
            return new Setup(db);
        }

        #endregion

        [Test]
        public async Task HasAccessAsync_Should_Return_Expected_ResultTrueforReportAllocated()
        {
            // Arrange
            using var deps = Initialize();
            var service = new BillingService(deps.Db);
            var dbUser = User.Create("Test User", "dev@cryptotrader.tax");

            var devReport2019 = Report.Create(
                taxYear: 2019,
                tradeCount: 4500,
                incomingTxCount: 5,
                outgoingTxCount: 1000,
                netRealizedGains: 632.60m);

            dbUser.Reports.Add(devReport2019);

            deps.Db.Users.Add(dbUser);
            deps.Db.SaveChanges();


            // Act
            BillingContext billingContext = new BillingContext(dbUser, devReport2019);
            var access = await service.HasAccessAsync(billingContext, default);

            // Assert
            access.Should().BeTrue();
        }

        [Test]
        public async Task HasAccessAsync_Should_Return_Expected_ResultFalseforReportNotAllocated()
        {
            // Arrange
            using var deps = Initialize();
            var service = new BillingService(deps.Db);
            var dbUser = User.Create("Test User", "dev@cryptotrader.tax");

            //Scenario where create 2 reports but only 2019 is allocated to user and 2020 is not allocated.
            var devReport2019 = Report.Create(
                taxYear: 2019,
                tradeCount: 4500,
                incomingTxCount: 5,
                outgoingTxCount: 1000,
                netRealizedGains: 632.60m);

            var devReport2020 = Report.Create(
               taxYear: 2020,
               tradeCount: 4500,
               incomingTxCount: 5,
               outgoingTxCount: 1000,
               netRealizedGains: 632.60m);

            dbUser.Reports.Add(devReport2019);

            deps.Db.Users.Add(dbUser);
            deps.Db.SaveChanges();


            // Act
            BillingContext billingContext = new BillingContext(dbUser, devReport2020);
            var access = await service.HasAccessAsync(billingContext, default);

            // Assert
            access.Should().BeFalse();
        }
    }
}