using System;
using System.Data.Entity.Migrations;
using TEMPUS.DB.Models;

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
            this.CreateSuperuser(context);
        }

        private void CreateSuperuser(DataContext context)
        {
            string login = "admin";
            string password = "admin1111";

            context.Users.AddOrUpdate(x => x.Login,
                new User()
                {
                    Id = Guid.NewGuid(),
                    Login = login,
                    Password = password
                });
            context.SaveChanges();
        }
    }
}
