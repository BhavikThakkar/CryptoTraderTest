using CryptoTax.Web.Features.Billing.Models;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoTax.Web.Features.Billing.Abstractions
{
    public interface IPaymentService
    {
        /// <summary>
        /// Purchases a report given a <see cref="PurchaseRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ValidationException">on invalid request.</exception>
        /// <returns>A <see cref="PurchaseResult"/></returns>
        Task<PurchaseResult> PurchaseAsync(PurchaseRequest request, CancellationToken cancellationToken=default);
    }
}
