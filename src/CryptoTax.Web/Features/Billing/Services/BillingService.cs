using CryptoTax.Data.Context;
using CryptoTax.Data.Entities;
using CryptoTax.Web.Features.Billing.Abstractions;
using CryptoTax.Web.Features.Billing.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoTax.Web.Features.Billing.Services
{
    public sealed class BillingService : IBillingService
    {
        private readonly CryptoTaxContext _db;

        public BillingService(CryptoTaxContext db)
        {
            _db = db;
        }


        /// <inheritdoc/>
        public Task<bool> HasAccessAsync(BillingContext context, CancellationToken cancellationToken)
        {
            bool hasAccess = false;

            if (context != null && context.Report != null)
                hasAccess = context.User.Reports.ToList().Exists(x => x.Id == context.Report.Id);

            return Task.FromResult(hasAccess);
        }

        /// <inheritdoc/>
        public Task<int> CalculatePriceAsync(BillingContext context, CancellationToken cancellationToken)
        {
            //take current usage on base of user's existing Reports
            int tradeCount = context.User.Reports.ToList().Sum(x => x.TradeCount);

            //add current report trade count to get new billing criteria matching threshold range 
            tradeCount += context.Report.TradeCount;

            //get best billing tier on base of trade count 
            BillingTier billingTier = _db.BillingTiers.Where(x => x.Threshold > tradeCount).
                                      OrderBy(x1 => x1.Threshold).
                                      FirstOrDefault();

            return Task.FromResult(billingTier.FullAmountInCents);

        }
    }
}
