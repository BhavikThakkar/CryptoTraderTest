using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace CryptoTax.Data.Entities
{
    public class User
    {
        #region EF Ctor

        protected User(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
            Reports = new List<Report>();
            Purchases = new List<Purchase>();
        }

        #endregion

        public static User Create(string name, string email)
        {
            return new User(
                id: Guid.NewGuid(),
                name: name,
                email: email);
        }

        /// <summary>
        /// User identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Full name of the user.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Email of the user.
        /// </summary>
        public string Email { get; }

        #region Navigation Properties

        /// <summary>
        /// Collection of the user's <see cref="Report"/>(s).
        /// </summary>
        public virtual ICollection<Report> Reports { get; private set; }

        /// <summary>
        /// Collection of the user's completed <see cref="Purchase"/>(s).
        /// </summary>
        public virtual ICollection<Purchase> Purchases { get; private set; }

        #endregion
    }

    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(o => o.Id);

            builder
                .Property(o => o.Name)
                .IsRequired();

            builder
                .Property(o => o.Email)
                .IsRequired();

            builder
                .HasMany(o => o.Reports)
                .WithOne()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(o => o.Purchases)
                .WithOne()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
