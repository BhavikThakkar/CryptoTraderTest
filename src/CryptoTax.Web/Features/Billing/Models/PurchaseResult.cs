using System;

namespace CryptoTax.Web.Features.Billing.Models
{
    public sealed class PurchaseResult
    {
        public PurchaseResult(Guid purchaseId, DateTimeOffset timestamp, int amountChargedInCents)
        {
            PurchaseId = purchaseId;
            Timestamp = timestamp;
            AmountChargedInCents = amountChargedInCents;
        }

        public Guid PurchaseId { get; }
        public DateTimeOffset Timestamp { get; }
        public int AmountChargedInCents { get; }
    }
}
