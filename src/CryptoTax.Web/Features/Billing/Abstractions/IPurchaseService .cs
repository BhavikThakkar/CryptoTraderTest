using CryptoTax.Data.Entities;
using CryptoTax.Web.Features.Billing.Models;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoTax.Web.Features.Billing.Abstractions
{
    public interface IPurchaseService
    {
        /// <summary>
        /// Make Payment & Purchases a report given a <see cref="PurchaseRequest"/>.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="report"></param>
        /// <param name="payrequest"></param>
        /// <param name="purchaserequest"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ValidationException">on invalid request.</exception>
        /// <returns>A <see cref="bool"/></returns>
        Task<bool> PurchaseAsync(User user, Report report, PaymentRequest payrequest,PurchaseRequest purchaserequest, CancellationToken cancellationToken=default);
    }
}
