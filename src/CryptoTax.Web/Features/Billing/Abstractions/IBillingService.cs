using CryptoTax.Web.Features.Billing.Models;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoTax.Web.Features.Billing.Abstractions
{
    public interface IBillingService
    {
        /// <summary>
        /// Determines if the user has access to the given report.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Indicator whether or now the requested user has access to the report.</returns>
        Task<bool> HasAccessAsync(BillingContext context, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calculates the purchase price of a given report.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The price of the report in USD cents.</returns>
        Task<int> CalculatePriceAsync(BillingContext context, CancellationToken cancellationToken = default);
    }
}
