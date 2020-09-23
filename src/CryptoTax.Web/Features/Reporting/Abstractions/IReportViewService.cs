using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CryptoTax.Data.Entities;

namespace CryptoTax.Web.Features.Reporting.Abstractions
{
    public interface IReportViewService
    {
        /// <summary>
        /// Retrieves a list of <see cref="Report"/>(s) for the given user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        Task<IOrderedEnumerable<Report>> GetReportsAsync(User user, CancellationToken cancellationToken = default);
    }
}
