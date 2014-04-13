using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
using TEMPUS.BaseDomain.Infrastructure;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.Infrastructure.Unity;

namespace TEMPUS.Infrastructure.Commands
{
    public static class AutomaticCommandHandlers
    {
        /// <summary>
        /// Automatically registers command handlers
        /// </summary>
        /// <param name="commandHandlersAssemblies">assemblies to look for command handlers in</param>
        /// <param name="bus">message bus</param>
        public static void Register(
            IEnumerable<Assembly> commandHandlersAssemblies,
            InMemoryBus bus)
        {
            //Argument.ExpectNotNull(() => commandHandlersAssemblies);
            //Argument.ExpectNotNull(() => bus);

            //among classes in command handlers assemblies select any which handle commands
            var cmdHandlerTypesWithCommands = commandHandlersAssemblies
                                        .SelectMany(a => a.GetTypes())
                                        .Where(t => !t.IsInterface && t.GetInterfaces()
                                            .Any(i => i.IsGenericType
                                                && i.GetGenericTypeDefinition() == typeof(IHandle<>)
                                                && typeof(ICommand).IsAssignableFrom(i.GetGenericArguments().First())))
                                        .Select(t => new
                                                    {
                                                        HandlerType = t,
                                                        CommandTypes = t.GetInterfaces().First().GetInterfaces().Select(i => i.GetGenericArguments().First())
                                                    });


            
            //foreach (var handler in cmdHandlerTypesWithCommands.Where(x => !String.Equals(x.HandlerType.Name, "OrderIntegrationProcess", StringComparison.OrdinalIgnoreCase)))
            foreach (var handler in cmdHandlerTypesWithCommands)
            {
                //object handlerInstance = Container.Current.Resolve(handler.HandlerType);
                object handlerInstance = Container.Current.Resolve(handler.HandlerType);

                foreach (Type cmdType in handler.CommandTypes)
                {
                    MethodInfo handleMethod = handler.HandlerType
                                                .GetMethods()
                                                .Where(m => m.Name.Equals("Handle", StringComparison.OrdinalIgnoreCase))
                                                .First(m => cmdType.IsAssignableFrom(m.GetParameters().First().ParameterType));

                    Type actionType = typeof(Action<>).MakeGenericType(cmdType);
                    Delegate handlerDelegate = Delegate.CreateDelegate(actionType, handlerInstance, handleMethod);

                    MethodInfo genericRegister = bus.GetType().GetMethod("RegisterHandler").MakeGenericMethod(new[] { cmdType });
                    genericRegister.Invoke(bus, new[] { handlerDelegate });
                }
            }
        }
    }
}