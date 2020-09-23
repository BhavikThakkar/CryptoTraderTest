using CryptoTax.Data.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace CryptoTax.Test.Data.Entities.Billing
{
    [TestFixture]
    public class BillingTierTest
    {
        [Test]
        public void Create_Should_Correctly_Build_Billing_Tier()
        {
            // Act
            var tier = BillingTier.Create(
                threshold: 100,
                name: "Hobbyist",
                fullAmountInCents: 3999);

            // Assert
            tier.Should().NotBeNull();
            tier.Id.Should().Should().NotBe(Guid.Empty);
            tier.Threshold.Should().Be(100);
            tier.FullAmountInCents.Should().Be(3999);
        }
    }
}
