using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CryptoTax.Data.Entities;
using CryptoTax.Web.Features.Reporting.Abstractions;

namespace CryptoTax.Web.Features.Reporting.Services
{
    public class ReportViewService : IReportViewService
    {
        /// <inheritdoc/>
        public Task<IOrderedEnumerable<Report>> GetReportsAsync(User user, CancellationToken cancellationToken = default)
        {
            if (user is null)
                throw new ArgumentNullException($"{nameof(user)}");

            // Retrive Reports
            var reports = user.Reports.OrderBy(o => o.TaxYear);

            return Task.FromResult(reports);
        }
    }
}
