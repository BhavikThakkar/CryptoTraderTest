using FluentValidation;
using System;

namespace CryptoTax.Web.Features.Billing.Models
{
    public sealed class PurchaseRequest
    {
        public PurchaseRequest(Guid userId, int amountInCents)
        {
            UserId = userId;
            AmountInCents = amountInCents;
        }

        public Guid UserId { get; }
        public int AmountInCents { get; }
    }

    public sealed class PurchaseRequestValidator : AbstractValidator<PurchaseRequest>
    {
        public PurchaseRequestValidator()
        {
            RuleFor(o => o.UserId).NotEmpty();
            RuleFor(o => o.AmountInCents).GreaterThan(0);
        }
    }
}
