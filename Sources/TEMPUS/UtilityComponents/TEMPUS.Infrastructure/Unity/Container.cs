using System;
using Microsoft.Practices.Unity;

namespace TEMPUS.Infrastructure.Unity
{
    public static class Container
    {
        private static IUnityContainer _container;

        #region public properties

        /// <summary>
        /// Get current container instance.
        /// </summary>
        public static IUnityContainer Current
        {
            get
            {
                if (_container == null)
                {
                    throw new InvalidOperationException("Unity container is not initialized.");
                }

                return _container;
            }
        }

        /// <summary>
        /// Gets value indicating whether container is initialized
        /// </summary>
        public static bool IsInitialized
        {
            get
            {
                return _container != null;
            }
        }

        /// <summary>
        /// Get resolved instance of object.
        /// </summary>
        /// <typeparam name="T">type of object</typeparam>
        /// <returns>Resolved instance of object</returns>
        public static T Get<T>()
        {
            return Current.Resolve<T>();
        }

        /// <summary>
        /// Get resolved instance of object by name.
        /// </summary>
        /// <typeparam name="T">type of object</typeparam>
        /// <param name="name">object name</param>
        /// <returns>Resolved instance of object</returns>
        public static T Get<T>(string name)
        {
            return Current.Resolve<T>(name);
        }

        #endregion

        /// <summary>
        /// Adds class to the container using Static container approach.
        /// </summary>
        /// <typeparam name="TTYpe">Marker interface for class isntance.</typeparam>
        /// <typeparam name="TInstance">Class implenting the <see cref="TTYpe"/>.</typeparam>
        public static void Add<TTYpe, TInstance>() where TInstance : class, TTYpe
        {
            Current.RegisterType<TTYpe, TInstance>(new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// Adds class to the container using Static container approach.
        /// </summary>
        /// <param name="name">alias name to use in type registration</param>
        /// <typeparam name="TTYpe">Marker interface for class isntance.</typeparam>
        /// <typeparam name="TInstance">Class implenting the <see cref="TTYpe"/>.</typeparam>
        public static void Add<TTYpe, TInstance>(string name, bool createNewEveryTime = false) where TInstance : class, TTYpe
        {
            LifetimeManager lifetimeManager;

            if (createNewEveryTime)
            {
                lifetimeManager = new TransientLifetimeManager();
            }
            else
            {
                lifetimeManager = new ContainerControlledLifetimeManager();
            }

            Current.RegisterType<TTYpe, TInstance>(name, lifetimeManager);
        }

        /// <summary>
        /// Adds class instance to the container using Static container approach.
        /// </summary>
        /// <typeparam name="TTYpe">Marker interface for class isntance.</typeparam>
        /// <remarks>Marker interface must be explicitly added to method call always in order to reduce issues with incorrect type recognition.</remarks>
        public static void Add<TTYpe>(TTYpe instance)
        {
            Current.RegisterInstance(instance, new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// Adds class instance to the container using Static container approach.
        /// </summary>
        /// <typeparam name="TTYpe">Marker interface for class isntance.</typeparam>
        /// <remarks>Marker interface must be explicitly added to method call always in order to reduce issues with incorrect type recognition.</remarks>
        public static void Add<TTYpe>(string name, TTYpe instance)
        {
            Current.RegisterInstance(name, instance, new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// Configures the MVC Unity container with the Uity container provided.
        /// </summary>
        public static void Init(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
        }

        /// <summary>
        /// Clear and dispose container object.
        /// </summary>
        public static void Clear()
        {
            if (_container != null)
            {
                _container.Dispose();
            }

            _container = null;
        }
    }
}