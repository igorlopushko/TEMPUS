using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TEMPUS.BaseDomain.Messages;

namespace TEMPUS.BaseDomain.Infrastructure
{
    public class InMemoryBus : IBus
    {
        private readonly Dictionary<Type, List<Action<IMessage>>> _handlers = new Dictionary<Type, List<Action<IMessage>>>();

        public void RegisterHandler<T>(Action<T> handler) where T : IMessage
        {
            List<Action<IMessage>> handlers;
            if (!_handlers.TryGetValue(typeof(T), out handlers))
            {
                handlers = new List<Action<IMessage>>();
                _handlers.Add(typeof(T), handlers);
            }

            handlers.Add(DelegateAdjuster.CastArgument<IMessage, T>(x => handler(x)));
        }

        public virtual void Send<T>(T command) where T : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException();
            }

            List<Action<IMessage>> handlers;
            if (_handlers.TryGetValue(command.GetType(), out handlers))
            {
                if (handlers.Count > 1)
                {
                    throw new InvalidOperationException("cannot send to more than one handler");
                }

                handlers[0](command);
            }
            else
            {
                throw new InvalidOperationException(string.Format("no handler registered for command: {0}", command.GetType().Name));
            }
        }

        public virtual void Publish<T>(T @event) where T : IEvent
        {
            List<Action<IMessage>> handlers;
            if (!_handlers.TryGetValue(@event.GetType(), out handlers)) return;
            foreach (Action<IMessage> handler in handlers)
            {
                Action<IMessage> handler1 = handler;
                //ThreadPool.QueueUserWorkItem(x => handler1(@event));

                //event handling should be synchronous currently
                handler1(@event);
            }
        }
    }
}