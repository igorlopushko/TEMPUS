using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using TEMPUS.DB.Models.Project;
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
            CreateRoles(context);
            CreateTestUsers(context);
            CreateDepartments(context);
            CreatePpsClassifications(context);
            CreateProjectRoles(context);
        }

        private void CreateRoles(DataContext context)
        {
            var roles = new List<string>
            {
                "Administrator",
                "Management",
                "ProjectManager",
                "User"
            };

            foreach (string role in roles)
            {
                context.Roles.AddOrUpdate(x => x.Name, new Role { Name = role });
            }
            context.SaveChanges();
        }

        private void CreateTestUsers(DataContext context)
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

        private void CreateDepartments(DataContext context)
        {
            var deparments = new List<string>
            {
                "Departament1",
                "Departament2",
                "Departament3"
            };

            foreach (string dept in deparments)
            {
                context.Departments.AddOrUpdate(x => x.Name, new Department { Name = dept });
            }
            context.SaveChanges();
        }

        private void CreatePpsClassifications(DataContext context)
        {
            var classifications = new Dictionary<string, int>
            {
                {"Assignment", 1},
                {"Mini", 2},
                {"Medium", 3},
                {"Mega", 4}
            };
            
            foreach (var c in classifications)
            {
                context.PpsClassifications.AddOrUpdate(x => x.Name, new PpsClassification { Name = c.Key, Weight = c.Value});
            }
            context.SaveChanges();
        }

        private void CreateProjectRoles(DataContext context)
        {
            var roles = new List<string>
            {
                "Owner",
                "Manager",
                "TeamMember"
            };

            foreach (string role in roles)
            {
                context.ProjectRoles.AddOrUpdate(x => x.Name, new ProjectRole { Name = role });
            }
            context.SaveChanges();
        }
    }
}