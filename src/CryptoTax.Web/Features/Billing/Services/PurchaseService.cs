using CryptoTax.Data.Context;
using CryptoTax.Data.Entities;
using CryptoTax.Web.Features.Billing.Abstractions;
using CryptoTax.Web.Features.Billing.Models;
using FluentValidation;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoTax.Web.Features.Billing.Services
{
    public sealed class PurchaseService : IPurchaseService
    {
        private readonly IValidator<PaymentRequest> _validator;
        private readonly IPaymentService _paymentService;
        private readonly CryptoTaxContext _db;
        public PurchaseService(IValidator<PaymentRequest> validator, IPaymentService paymentService, CryptoTaxContext db)
        {
            _validator = validator;
            _paymentService = paymentService;
            _db = db;
        }

        /// <inheritdoc/>
        public async Task<bool> PurchaseAsync(User user, Report report, PaymentRequest payrequest, PurchaseRequest purchaserequest, CancellationToken cancellationToken)
        {
            // Validate
            _validator.ValidateAndThrow(payrequest);

            //TODO - here payment check - needs to be done with Card Number, CVV & ExpDate can be performed here, if payment goes well, then proceed further.

            //write purchase logic here
            var result = await _paymentService.PurchaseAsync(purchaserequest);

            //on successful transaction add report to user's stack
            if (result.PurchaseId != Guid.Empty )
            {
                await AssignReportToUserAsync(user, report);
            }
            return true;
        }

        public async Task AssignReportToUserAsync(User user, Report report)
        {
            //Check if report is already not assigned to user -  this shall not be practical case but with current sample its needed 
            bool isReportAlreadyAllocated = user.Reports.ToList().Exists(x => x.Id == report.Id);

            if (!isReportAlreadyAllocated)
            {
                user.Reports.Add(report);
                await _db.SaveChangesAsync();
            }
        }
    }
}
