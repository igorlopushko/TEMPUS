using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using TEMPUS.BaseDomain.Infrastructure;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.BaseDomain.Model.ServiceLayer;
using TEMPUS.DB;
using TEMPUS.Infrastructure.Commands;
using TEMPUS.Infrastructure.Unity;
using TEMPUS.ProjectDomain.Infrastructure;
using TEMPUS.ProjectDomain.Model.DomainLayer;
using TEMPUS.ProjectDomain.Services;
using TEMPUS.ProjectDomain.Services.DomainLayer;
using TEMPUS.UserDomain.Infrastructure;
using TEMPUS.UserDomain.Model.DomainLayer;
using TEMPUS.UserDomain.Services.DomainLayer;
using TEMPUS.UserDomain.Services.ServiceLayer;
using TEMPUS.WebSite.Controllers;
using TEMPUS.WebSite.Interfaces;
using TEMPUS.WebSite.Services;

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
            Container.Add<IFormsAuthenticationService, FormsAuthenticationService>();
            Container.Add<IMembershipService, AccountMembershipService>();
            Container.Add<IProjectQueryService, ProjectQueryService>();

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
            // TODO: Review! It's highly recommended to use one DataContext per request to avoid high memory consumption.
            var context = new DataContext();

            Container.Add<IUserStorage<DB.Models.User.User>>(new UserStorage(context));
            Container.Add<IProjectStorage<DB.Models.Project.Project>>(new ProjectStorage(context));

            var userRepository = new UserRepository(Container.Get<IEventStore>(),
                Container.Get<IUserStorage<DB.Models.User.User>>());
            Container.Add<IRepository<User, UserId>>(userRepository);

            Container.Add(new UserCommandService(userRepository));

            var projectRepository = new ProjectRepository(Container.Get<IEventStore>(),
                Container.Get<IProjectStorage<DB.Models.Project.Project>>());
            Container.Add<IRepository<Project, ProjectId>>(projectRepository);

            Container.Add(new ProjectCommandService(projectRepository));

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
