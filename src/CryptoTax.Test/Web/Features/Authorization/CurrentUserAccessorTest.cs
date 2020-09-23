using System;
using System.Threading.Tasks;
using CryptoTax.Data.Context;
using CryptoTax.Data.Entities;
using CryptoTax.Web.Features.Authorization.Services;
using FluentAssertions;
using NUnit.Framework;
using static CryptoTax.Test.Extensions.InMemoryContextExtensions;

namespace CryptoTax.Test.Web.Features.Authorization
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class CurrentUserAccessorTest
    {
        #region Setup

        private sealed class Setup : IDisposable
        {
            public Setup(CryptoTaxContext dbContext)
            {
                Db = dbContext;
            }

            public CryptoTaxContext Db;

            public void Dispose()
            {
                Db?.Dispose();
            }
        }

        private Setup Initialize()
        {
            var db = CreateTestContext();
            return new Setup(db);
        }

        #endregion

        [Test]
        public async Task GetUserAsync_Should_Return_Expected_User()
        {
            // Arrange
            using var deps = Initialize();
            var service = new CurrentUserAccessor(deps.Db);
            var dbUser = User.Create("Test User", "dev@cryptotrader.tax");

            deps.Db.Users.Add(dbUser);
            deps.Db.SaveChanges();
            
            // Act
            var user = await service.GetUserAsync(default);
            
            // Assert
            user.Should().NotBeNull();
            user.Name.Should().Be("Test User");
            user.Email.Should().Be("dev@cryptotrader.tax");
        }

        [Test]
        public void GetUserAsync_Should_Throw_When_User_Does_Not_Exist()
        {
            // Arrange
            using var deps = Initialize();
            var service = new CurrentUserAccessor(deps.Db);
            
            // Act
            Func<Task> action = async () => await service.GetUserAsync(default);
            action.Invoke();
            
            // Assert
            action.Should().Throw<InvalidOperationException>();
        }
    }
}