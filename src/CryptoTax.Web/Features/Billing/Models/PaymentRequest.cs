using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace CryptoTax.Web.Features.Billing.Models
{
    public sealed class PaymentRequest
    {
        public PaymentRequest(Guid userId, int amountInCents)
        {
            UserId = userId;
            AmountInCents = amountInCents;
        }
        [Required, CreditCard, Display(Name = "Card Number")]
        public string CreditCardNumber { get; set; }

        [Required, Display(Name = "Expiration Month"), Range(0, 12)]
        public long? CardExpiryMonth { get; set; }

        [Required, Display(Name = "Expiration Year"), Range(2020, 2100)]
        public long? CardExpiryYear { get; set; }

        [Required, Display(Name = "CVC Security Code"),
         RegularExpression("^[0-9]*$", ErrorMessage = "CVC security code can only contain numbers")]
        public string CardCvc { get; set; }
        [Required]
        public int AmountInCents { get; }
        public Guid UserId { get; }
    }

    public sealed class PaymentRequestValidator : AbstractValidator<PaymentRequest>
    {
        public PaymentRequestValidator()
        {
            RuleFor(o => o.UserId).NotEmpty();
            RuleFor(o => o.AmountInCents).GreaterThan(0);
        }
    }
}
