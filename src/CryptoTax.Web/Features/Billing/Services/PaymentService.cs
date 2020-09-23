using CryptoTax.Web.Features.Billing.Abstractions;
using CryptoTax.Web.Features.Billing.Models;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoTax.Web.Features.Billing.Services
{
    public sealed class PaymentService : IPaymentService
    {
        private readonly IValidator<PurchaseRequest> _validator;

        public PaymentService(IValidator<PurchaseRequest> validator)
        {
            _validator = validator;
        }

        /// <inheritdoc/>
        public Task<PurchaseResult> PurchaseAsync(PurchaseRequest request, CancellationToken cancellationToken)
        {
            // Validate
            _validator.ValidateAndThrow(request);

            // In a production scenrio this would make an external request to our billing provider such as Stripe.
            var stripeChargedAmount = request.AmountInCents;

            // Build Result
            var result = new PurchaseResult(
                purchaseId: Guid.NewGuid(),
                timestamp: DateTimeOffset.UtcNow,
                amountChargedInCents: stripeChargedAmount);

            return Task.FromResult(result);
        }
    }
}
