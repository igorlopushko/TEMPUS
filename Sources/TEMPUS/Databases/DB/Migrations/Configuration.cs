using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using TEMPUS.DB.Models.User;

namespace TEMPUS.DB.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TEMPUS.DB.DataContext";
        }

        protected override void Seed(DataContext context)
        {
            List<string> roles = new List<string>()
            {
                "Admin",
                "Director",
                "Manager",
                "Asignee"
            };

            this.CreateRoles(context, roles);
            this.CreateTestUsers(context, roles);
        }

        private void CreateRoles(DataContext context, IEnumerable<string> roles)
        {
            foreach (string role in roles)
            {
                context.Roles.AddOrUpdate(x => x.Name, new Role() { Name = role });
            }
            context.SaveChanges();
        }

        private void CreateTestUsers(DataContext context, IEnumerable<string> roles)
        {
            foreach (string role in roles)
            {
                context.Users.AddOrUpdate(x => x.Login,
                    new User()
                    {
                        Login = role,
                        Password = "test1234",
                        RoleId = context.Roles.First(x => x.Name == role).Id
                    });
            }

            context.SaveChanges();
        }
    }
}
