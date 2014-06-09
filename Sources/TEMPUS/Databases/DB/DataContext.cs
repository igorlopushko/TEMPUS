using System.Data.Entity;
using TEMPUS.DB.Migrations;
using TEMPUS.DB.Models.Project;
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

        
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoleRelation> UserRoleRelations { get; set; }
        public DbSet<UserMood> Moods { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectRole> ProjectRoles { get; set; }
        public DbSet<ProjectRoleRelation> ProjectRoleRelations { get; set; }
        public DbSet<PpsClassification> PpsClassifications { get; set; }
        public DbSet<TimeRecord> TimeRecords { get; set; }

        /// <summary>
        /// Configure model properties for mapping to database
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}