using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Reflection;
using System.Web.Mvc;
using TEMPUS.BaseDomain.Infrastructure;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.BaseDomain.Model.ServiceLayer;
using TEMPUS.DB;
using TEMPUS.Infrastructure.Commands;
using TEMPUS.Infrastructure.Unity;
using TEMPUS.UserDomain.Infrastructure;
using TEMPUS.UserDomain.Model.DomainLayer;
using TEMPUS.UserDomain.Services.DomainLayer;
using TEMPUS.UserDomain.Services.ServiceLayer;
using TEMPUS.WebSite.Controllers;

namespace TEMPUS.WebSite.Helpers
{
    public class Bootstrapper
    {
        public static void ConfigureUnityContainer()
        {
            Database.SetInitializer<DataContext>(null);

            Container.Init(new UnityContainer());

            Container.Add<IController, ErrorController>("ErrorController", true);

            // Registrations
            Container.Add<IUserQueryService, UserQueryService>();

            var bus = new InMemoryBus();
            Container.Add<ICommandSender>(bus);
            Container.Add<IEventPublisher>(bus);

            var eventStore = new InMemoryEventStore();
            Container.Add<IEventStore>(eventStore);

            RegisterCommandHandlers(bus);
            RegisterEventHandlers(bus);
        }

        private static void RegisterCommandHandlers(InMemoryBus bus)
        {
            // TODO: Move database creation from here.
            var context = new DataContext();
            if (!context.Database.Exists())
            {
                // Create the SimpleMembership database without Entity Framework migration schema
                ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
            }
            
            // TODO: Remove. This is only for test purposes, for checking if DB is created.
            context.Users.Add(new DB.Models.User
            {
                Id = Guid.NewGuid(),
                Login = "SomeLogin",
                Password = "SomePass"
            });
            context.SaveChanges();

            Container.Add<IUserStorage<DB.Models.User>>(new UserStorage(context));
            Container.Add<IUserStorage<DB.Models.User>>(new UserStorage(context));

            var customerRepository = new UserRepository(Container.Get<IEventStore>(),
                Container.Get<IUserStorage<DB.Models.User>>());
            Container.Add<IRepository<User, UserId>>(customerRepository);

            Container.Add(new UserCommandService(customerRepository));

            var commandHandlersAssemblies = new List<Assembly>
            {
                //assemblies with command handlers
                Assembly.Load(new AssemblyName("TEMPUS.UserDomain.Services"))
            };

            AutomaticCommandHandlers.Register(commandHandlersAssemblies, bus);
        }

        private static void RegisterEventHandlers(InMemoryBus bus)
        {
            IUserEventHandler userEventHandler = new UserEventHandler();

            bus.RegisterHandler<UserCreated>(userEventHandler.Handle);
        }
    }
}