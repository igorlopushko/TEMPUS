using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using TEMPUS.BaseDomain.Infrastructure;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.BaseDomain.Model.ServiceLayer;
using TEMPUS.Infrastructure.Commands;
using TEMPUS.Infrastructure.Unity;
using TEMPUS.UserDomain.Infrastructure;
using TEMPUS.UserDomain.Model.DomainLayer;
using TEMPUS.UserDomain.Services.DomainLayer;
using TEMPUS.UserDomain.Services.ServiceLayer;
using TEMPUS.WebSite.Controllers;
using TEMPUS.UserDomain.Model.ServiceLayer;

namespace TEMPUS.WebSite.Helpers
{
    public class Bootstrapper
    {
        public static void ConfigureUnityContainer()
        {
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
            var context = new UserDataContext();
            
            Container.Add<IUserStorage<User, UserId>>(new UserStorage(context));
            Container.Add<IUserReadStorage<UserInfo>>(new UserReadStorage(context));
            Container.Add<IUserStorage<User, UserId>>(new UserStorage(context));
            Container.Add<IUserReadStorage<UserInfo>>(new UserReadStorage(context));
            Container.Add<IUserReadRepository>(new UserReadRepository(Container.Get<IUserReadStorage<UserInfo>>()));

            var customerRepository = new UserRepository(Container.Get<IEventStore>(), Container.Get<IUserStorage<User, UserId>>());
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