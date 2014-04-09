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
using TEMPUS.Infrastructure.Commands;
using TEMPUS.Infrastructure.Unity;
using TEMPUS.UserDomain.Infrastructure;
using TEMPUS.UserDomain.Services;
using TEMPUS.WebSite.Controllers;

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
            

            RegisterCommandHandlers(bus);
        }

        private static void RegisterCommandHandlers(InMemoryBus bus)
        {
            //var customerRepo = new UserRepository(Container.Get<IEventStore>(), Container.Get<IUserQueryService>());
            //Container.Add<IRepository<User, UserId>>(customerRepo);

            //var orderRepo = new OrderRepository(
            //                        Container.Get<IEventStore>(),
            //                        Container.Get<IOrderQueryService>());
            //Container.Add<IOrderRepository>(orderRepo);

            //Container.Add<IUserIdGeneratorService, NewGuidUserIdGeneratorService>();

            var commandHandlersAssemblies = new List<Assembly>
            {
                //assemblies with command handlers
                Assembly.Load(new AssemblyName("TEMPUS.UserDomain.Services"))
            };

            AutomaticCommandHandlers.Register(commandHandlersAssemblies, bus);
        }
    }
}