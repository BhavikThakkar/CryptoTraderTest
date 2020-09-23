using CryptoTax.Data.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace CryptoTax.Test.Data.Entities.User
{
    [TestFixture]
    public class PurchaseTest
    {
        [Test]
        public void Create_Should_Correctly_Build_Purchase()
        {
            // Arrange
            var year = 2020;
            var price = 4999;
            var tier = BillingTier.Create(
                name: "Hobbyist",
                threshold: 100,
                fullAmountInCents: price);

            // Act
            var purchase = Purchase.Create(
                taxYear: year,
                timestamp: DateTimeOffset.UtcNow,
                amountInCents: price,
                billingTier: tier);

            // Assert
            purchase.Should().NotBeNull();
            purchase.Id.Should().NotBe(Guid.Empty);
            purchase.TaxYear.Should().Be(2020);
            purchase.AmountInCents.Should().Be(4999);
            purchase.BillingTierId.Should().Be(tier.Id);
        }
    }
}
