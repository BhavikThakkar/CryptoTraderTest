using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CryptoTax.Data.Entities
{
    public class Report
    {
        #region EF Ctor

        protected Report(
            Guid id,
            int taxYear,
            int tradeCount,
            int incomingTxCount,
            int outgoingTxCount,
            decimal netRealizedGains)
        {
            Id = id;
            TaxYear = taxYear;
            TradeCount = tradeCount;
            IncomingTxCount = incomingTxCount;
            OutgoingTxCount = outgoingTxCount;
            NetRealizedGains = netRealizedGains;
        }

        #endregion

        public static Report Create(
            int taxYear,
            int tradeCount,
            int incomingTxCount,
            int outgoingTxCount,
            decimal netRealizedGains)
        {
            return new Report(
                id: Guid.NewGuid(),
                taxYear: taxYear,
                tradeCount: tradeCount,
                incomingTxCount: incomingTxCount,
                outgoingTxCount: outgoingTxCount,
                netRealizedGains: netRealizedGains);
        }

        /// <summary>
        /// Report identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Tax year for the report.
        /// </summary>
        public int TaxYear { get; }

        /// <summary>
        /// Number of trades included in this report.
        /// </summary>
        public int TradeCount { get; }

        /// <summary>
        /// Number of transactions included in this report.
        /// </summary>
        public int IncomingTxCount { get; }

        /// <summary>
        /// Number of transactions included in this report.
        /// </summary>
        public int OutgoingTxCount { get; }

        /// <summary>
        /// The calculated gains/losses on the report.
        /// </summary>
        public decimal NetRealizedGains { get; }
    }

    public class ReportConfig : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder
                .HasKey(o => o.Id);

            builder
                .Property(o => o.TaxYear)
                .IsRequired();

            builder
                .Property(o => o.TradeCount)
                .IsRequired();

            builder
                .Property(o => o.IncomingTxCount)
                .IsRequired();

            builder
                .Property(o => o.OutgoingTxCount)
                .IsRequired();

            builder
                .Property(o => o.NetRealizedGains)
                .IsRequired();
        }
    }
}
