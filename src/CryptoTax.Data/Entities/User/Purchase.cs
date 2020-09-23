using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CryptoTax.Data.Entities
{
    public class Purchase
    {
        #region EF Ctor

        protected Purchase(
            Guid id,
            int taxYear,
            DateTimeOffset timestamp,
            int amountInCents,
            Guid billingTierId)
        {
            Id = id;
            TaxYear = taxYear;
            Timestamp = timestamp;
            AmountInCents = amountInCents;
            BillingTierId = billingTierId;
        }

        #endregion

        public static Purchase Create(
            int taxYear,
            DateTimeOffset timestamp,
            int amountInCents,
            BillingTier billingTier)
        {
            return new Purchase(
                id: Guid.NewGuid(),
                taxYear: taxYear,
                timestamp: timestamp,
                amountInCents: amountInCents,
                billingTierId: billingTier.Id);
        }

        /// <summary>
        /// Purchase identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Tax year the report was purchased for.
        /// </summary>
        public int TaxYear { get; }

        /// <summary>
        /// UTC timestamp the purchase was made at.
        /// </summary>
        public DateTimeOffset Timestamp { get; }

        /// <summary>
        /// The amount in USD cents that was paid for this purchase.
        /// </summary>
        public int AmountInCents { get; }

        #region Navigation Properties

        /// <summary>
        /// The <see cref="BillingTier"/> for this purchase.
        /// </summary>
        public Guid BillingTierId { get; private set; }
        public virtual BillingTier Tier { get; private set; }

        #endregion
    }

    public class PurchaseConfig : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder
                .HasKey(o => o.Id);

            builder
                .Property(o => o.TaxYear)
                .IsRequired();

            builder
                .Property(o => o.Timestamp)
                .IsRequired();

            builder
                .Property(o => o.AmountInCents)
                .IsRequired();

            builder
                .HasOne(x => x.Tier)
                .WithMany()
                .HasForeignKey(x => x.BillingTierId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
