using System;
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
            var roles = new List<string>
            {
                "Administrator",
                "Director",
                "Manager",
                "TeamMember"
            };

            CreateRoles(context, roles);
            CreateTestUsers(context, roles);
        }

        private void CreateRoles(DataContext context, IEnumerable<string> roles)
        {
            foreach (string role in roles)
            {
                context.Roles.AddOrUpdate(x => x.Name, new Role { Name = role });
            }
            context.SaveChanges();
        }

        private void CreateTestUsers(DataContext context, IEnumerable<string> roles)
        {
            if (!context.Users.Any(x => x.Email == "igor.lopushko@sigmaukraine.com"))
            {
                var id = Guid.NewGuid();
                context.Users.AddOrUpdate(x => x.Email,
                    new User
                    {
                        Id = id,
                        Email = "igor.lopushko@sigmaukraine.com",
                        Password = "123123",
                        DateOfBirth = new DateTime(1987, 7, 15),
                        FirstName = "Igor",
                        LastName = "Lopushko",
                        Phone = "+38-097-923-36-67"
                    });
                var role = context.Roles.FirstOrDefault(x => x.Name == "Administrator");
                context.UserRoleRelations.Add(new UserRoleRelation {RoleId = role.Id, UserId = id});
            }

            context.SaveChanges();
        }
    }
}