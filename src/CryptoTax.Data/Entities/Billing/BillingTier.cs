using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CryptoTax.Data.Entities
{
    public class BillingTier
    {
        #region EF Ctor

        protected BillingTier(
            Guid id,
            string name,
            int threshold,
            int fullAmountInCents)
        {
            Id = id;
            Name = name;
            Threshold = threshold;
            FullAmountInCents = fullAmountInCents;
        }

        #endregion

        public static BillingTier Create(string name, int threshold, int fullAmountInCents)
        {
            return new BillingTier(
                id: Guid.NewGuid(),
                name: name,
                threshold: threshold,
                fullAmountInCents: fullAmountInCents);
        }

        /// <summary>
        /// Billing Tier identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Display name of the billing tier.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The transaction count threshold for this billing tier.
        /// </summary>
        public int Threshold { get; }

        /// <summary>
        /// The full USD amount in cents for this billing tier.
        /// </summary>
        public int FullAmountInCents { get; }
    }

    public class BillingTierConfig : IEntityTypeConfiguration<BillingTier>
    {
        public void Configure(EntityTypeBuilder<BillingTier> builder)
        {
            builder
                .HasKey(o => o.Id);

            builder
                .Property(o => o.Id)
                .IsRequired();

            builder
                .Property(o => o.Name)
                .IsRequired();

            builder
                .Property(o => o.Threshold)
                .IsRequired();

            builder
                .Property(o => o.FullAmountInCents)
                .IsRequired();
        }
    }
}
