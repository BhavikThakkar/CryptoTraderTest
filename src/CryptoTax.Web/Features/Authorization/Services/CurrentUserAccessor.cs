using CryptoTax.Data.Context;
using CryptoTax.Data.Entities;
using CryptoTax.Web.Features.Authorization.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoTax.Web.Features.Authorization.Services
{
    public sealed class CurrentUserAccessor : ICurrentUserAccessor
    {
        private readonly CryptoTaxContext _db;

        public CurrentUserAccessor(CryptoTaxContext db)
        {
            _db = db;
        }

        /// <inheritdoc/>
        public async Task<User> GetUserAsync(CancellationToken cancellationToken)
        {
            var user = await _db.Users
                .AsQueryable()
                .Where(o => o.Email == CryptoTaxContext.CURRENT_USER_EMAIL)
                .SingleOrDefaultAsync(cancellationToken);

            if (user is null)
                throw new InvalidOperationException("User is not authenticated.");

            return user;
        }
    }
}
