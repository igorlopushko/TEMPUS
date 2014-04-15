using System.Data.Entity;
using TEMPUS.UserDomain.Model.DomainLayer;
using TEMPUS.UserDomain.Model.ServiceLayer;

namespace TEMPUS.UserDomain.Infrastructure
{
    /// <summary>
    /// The class represents data context related to User.
    /// </summary>
    public class UserDataContext : DbContext
    {
        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the users info.
        /// </summary>
        public DbSet<UserInfo> UsersInfo { get; set; }

        /// <summary>
        /// Configure model properties for mapping to database
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Login).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(x => x.Phone).HasMaxLength(31);
            modelBuilder.Entity<User>().HasRequired(x => x.Password);
            modelBuilder.Entity<User>().HasRequired(x => x.Login);
        }
    }
}
