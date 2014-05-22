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
            CreateProjects(context);
            CreateProjectRoleRelations(context);
            CreateUserMoods(context);
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
                var id = Guid.Parse("70e64d96-93df-e311-be94-d850e64cba2a");
                context.Users.AddOrUpdate(x => x.Email,
                    new User
                    {
                        Id = id,
                        Email = "igor.lopushko@sigmaukraine.com",
                        Password = "4297f44b13955235245b2497399d7a93",
                        DateOfBirth = new DateTime(1987, 7, 15),
                        FirstName = "Igor",
                        LastName = "Lopushko",
                        Phone = "+380979233667",
                        IsDeleted = false
                    });
                var role = context.Roles.FirstOrDefault(x => x.Name == "Administrator");
                context.UserRoleRelations.Add(new UserRoleRelation { RoleId = role.Id, UserId = id });
            }
            if (!context.Users.Any(x => x.Email == "shatovska@gmail.com"))
            {
                var id = Guid.NewGuid();
                context.Users.AddOrUpdate(x => x.Email,
                    new User
                    {
                        Id = id,
                        Email = "shatovska@gmail.com",
                        Password = "4297f44b13955235245b2497399d7a93",
                        DateOfBirth = new DateTime(1975, 5, 10),
                        FirstName = "Tetyana",
                        LastName = "Shatovska",
                        Phone = "+380958252745",
                        IsDeleted = false
                    });
                var role = context.Roles.FirstOrDefault(x => x.Name == "ProjectManager");
                context.UserRoleRelations.Add(new UserRoleRelation { RoleId = role.Id, UserId = id });
            }
            if (!context.Users.Any(x => x.Email == "devoto13@gmail.com"))
            {
                var id = Guid.NewGuid();
                context.Users.AddOrUpdate(x => x.Email,
                    new User
                    {
                        Id = id,
                        Email = "devoto13@gmail.com",
                        Password = "4297f44b13955235245b2497399d7a93",
                        DateOfBirth = new DateTime(1993, 1, 13),
                        FirstName = "Yaroslav",
                        LastName = "Admin",
                        Phone = "+380939382461",
                        IsDeleted = false
                    });
                var role = context.Roles.FirstOrDefault(x => x.Name == "User");
                context.UserRoleRelations.Add(new UserRoleRelation { RoleId = role.Id, UserId = id });
            }
            if (!context.Users.Any(x => x.Email == "sanyazayats@gmail.com"))
            {
                var id = Guid.NewGuid();
                context.Users.AddOrUpdate(x => x.Email,
                    new User
                    {
                        Id = id,
                        Email = "sanyazayats@gmail.com",
                        Password = "4297f44b13955235245b2497399d7a93",
                        DateOfBirth = new DateTime(1993, 1, 1),
                        FirstName = "Alexander",
                        LastName = "Zayats",
                        Phone = "+380935048448",
                        IsDeleted = false
                    });
                var role = context.Roles.FirstOrDefault(x => x.Name == "User");
                context.UserRoleRelations.Add(new UserRoleRelation { RoleId = role.Id, UserId = id });
            }
            if (!context.Users.Any(x => x.Email == "anatoliy.ovchinnikov@sigmaukraine.com"))
            {
                var id = Guid.NewGuid();
                context.Users.AddOrUpdate(x => x.Email,
                    new User
                    {
                        Id = id,
                        Email = "anatoliy.ovchinnikov@sigmaukraine.com",
                        Password = "4297f44b13955235245b2497399d7a93",
                        DateOfBirth = new DateTime(1993, 7, 24),
                        FirstName = "Anatoliy",
                        LastName = "Ovchinnikov",
                        Phone = "+380956146247",
                        IsDeleted = false
                    });
                var role = context.Roles.FirstOrDefault(x => x.Name == "User");
                context.UserRoleRelations.Add(new UserRoleRelation { RoleId = role.Id, UserId = id });
            }
            if (!context.Users.Any(x => x.Email == "wer3452@gmail.com"))
            {
                var id = Guid.NewGuid();
                context.Users.AddOrUpdate(x => x.Email,
                    new User
                    {
                        Id = id,
                        Email = "wer3452@gmail.com",
                        Password = "4297f44b13955235245b2497399d7a93",
                        DateOfBirth = new DateTime(1994, 4, 30),
                        FirstName = "Dmitriy",
                        LastName = "Volkov",
                        Phone = "+380957558789",
                        IsDeleted = false
                    });
                var role = context.Roles.FirstOrDefault(x => x.Name == "User");
                context.UserRoleRelations.Add(new UserRoleRelation { RoleId = role.Id, UserId = id });
            }
            if (!context.Users.Any(x => x.Email == "alexandra.yugan@sigmaukraine.com"))
            {
                var id = Guid.NewGuid();
                context.Users.AddOrUpdate(x => x.Email,
                    new User
                    {
                        Id = id,
                        Email = "alexandra.yugan@sigmaukraine.com",
                        Password = "4297f44b13955235245b2497399d7a93",
                        DateOfBirth = new DateTime(1993, 1, 1),
                        FirstName = "Alexandra",
                        LastName = "Yugan",
                        Phone = "+380996588615",
                        IsDeleted = false
                    });
                var role = context.Roles.FirstOrDefault(x => x.Name == "User");
                context.UserRoleRelations.Add(new UserRoleRelation { RoleId = role.Id, UserId = id });
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
                context.PpsClassifications.AddOrUpdate(x => x.Name, new PpsClassification { Name = c.Key, Weight = c.Value });
            }
            context.SaveChanges();
        }

        private void CreateProjectRoles(DataContext context)
        {
            var roles = new List<string>
            {
                "Owner",
                "Manager",
                "Team member"
            };

            foreach (string role in roles)
            {
                context.ProjectRoles.AddOrUpdate(x => x.Name, new ProjectRole { Name = role });
            }
            context.SaveChanges();
        }

        private void CreateProjects(DataContext context)
        {
            Guid department = context.Departments.FirstOrDefault().Id;
            Guid pps = context.PpsClassifications.FirstOrDefault().Id;
            var projectId = Guid.NewGuid();
            context.Projects.AddOrUpdate(x => x.Name, new Project
            {
                Id = projectId,
                Name = "Tempus project",
                Description = "Management tool",
                ProjectOrderer = "IKEA",
                RecievingOrganization = "IKEA",
                Mandatory = true,
                DepartmentId = department,
                PpsClassificationId = pps,
                StartDate = new DateTime(2014, 3, 1),
                EndDate = new DateTime(2014, 6, 5),
                IsDeleted = false
            });
            context.SaveChanges();
        }

        private void CreateProjectRoleRelations(DataContext context)
        {
            Guid project = context.Projects.FirstOrDefault(x => x.Name == "Tempus project").Id;
            Guid manager = context.ProjectRoles.FirstOrDefault(x => x.Name == "Manager").Id;
            Guid teamMember = context.ProjectRoles.FirstOrDefault(x => x.Name == "Team member").Id;
            Guid shatovska = context.Users.FirstOrDefault(x => x.LastName == "Shatovska").Id;
            context.ProjectRoleRelations.AddOrUpdate(x => x.UserId, new ProjectRoleRelation
            {
                ProjectId = project,
                ProjectRoleId = manager,
                UserId = shatovska
            });
            foreach (var userId in context.Users.Where(x => x.Id != shatovska).Select(x => x.Id).ToArray())
            {
                context.ProjectRoleRelations.AddOrUpdate(x => x.UserId, new ProjectRoleRelation
                {
                    ProjectId = project,
                    ProjectRoleId = teamMember,
                    UserId = userId
                });
            }
            context.SaveChanges();
        }

        private void CreateUserMoods(DataContext context)
        {
            Random r = new Random();
            var userIds = context.Users.Select(x => x.Id).ToArray();
            foreach (var userId in userIds)
            {
                for (int i = 1; i < 8; i++)
                {
                    context.Moods.AddOrUpdate(x => new { x.Date, x.UserId }, new UserMood
                    {
                        UserId = userId,
                        Rate = r.Next(1, 5),
                        Date = DateTime.Now.Date.Subtract(TimeSpan.FromDays(i))
                    });
                }
            }
            context.SaveChanges();
        }
    }
}