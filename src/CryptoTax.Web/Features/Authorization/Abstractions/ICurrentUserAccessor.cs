using CryptoTax.Data.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoTax.Web.Features.Authorization.Abstractions
{
    public interface ICurrentUserAccessor
    {
        /// <summary>
        /// Retrieves the currently authenticated user from the data store.
        /// </summary>
        Task<User> GetUserAsync(CancellationToken cancellationToken = default);
    }
}
