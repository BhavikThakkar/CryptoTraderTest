using CryptoTax.Data.Entities;

namespace CryptoTax.Web.Features.Billing.Models
{
    public sealed class BillingContext
    {
        public BillingContext(User user, Report report)
        {
            User = user;
            Report = report;
        }

        /// <summary>
        /// The <see cref="User"/> for this request.
        /// </summary>
        public User User { get; }
        
        /// <summary>
        /// The <see cref="Report"/> for this request.
        /// </summary>
        public Report Report { get; }
    }
}
