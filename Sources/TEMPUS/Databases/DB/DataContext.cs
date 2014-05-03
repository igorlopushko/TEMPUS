using System.Data.Entity;
using TEMPUS.DB.Migrations;
using TEMPUS.DB.Models.User;

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
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRoleRelation> UserRoleRelations { get; set; }


        /// <summary>
        /// Configure model properties for mapping to database
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
