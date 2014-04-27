using System.Data.Entity;
using TEMPUS.DB.Migrations;
using TEMPUS.DB.Models;

namespace TEMPUS.DB
{
    /// <summary>
    /// The class represents data context.
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        static DataContext()
        {
            Database.SetInitializer<DataContext>(new MigrateDatabaseToLatestVersion<DataContext, Configuration>());
        }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Configure model properties for mapping to database
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            //modelBuilder.Entity<User>().Property(x => x.Login).HasMaxLength(50);
            //modelBuilder.Entity<User>().Property(x => x.Phone).HasMaxLength(31);
            //modelBuilder.Entity<User>().Property(x => x.Password).HasMaxLength(16);
            //modelBuilder.Entity<User>().HasRequired(x => x.Password);
            //modelBuilder.Entity<User>().HasRequired(x => x.Login);
        }
    }
}
