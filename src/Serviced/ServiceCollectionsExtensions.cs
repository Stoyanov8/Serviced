namespace Serviced
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class ServiceCollectionsExtensions
    {
        #region Extensions
        /// <summary>
        /// Registers all items for given assemblies
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiced(this IServiceCollection services, params Assembly[] assemblies)
        {
            var compatibleAssemblies = FilterAssemblies(assemblies);

            var servicesToRegister = compatibleAssemblies
                .SelectMany(x => x.GetTypes())
                .Where(t => typeof(IServiced).IsAssignableFrom(t))
                .ToList();

            foreach (var serviceToRegister in servicesToRegister)
            {
                var (serviceType, implementationType) = GetTypes(serviceToRegister);

                var lifetime = GetLifetime(serviceToRegister);

                if (typeof(IHasImplementationFactory).IsAssignableFrom(serviceToRegister))
                {
                    RegisterWithImplementationFactory(services, implementationType, lifetime);
                }
                else
                {
                    RegisterWithTypes(services, serviceType, implementationType, lifetime);
                }
            }

            return services;
        }

        /// <summary>
        /// Registers all items for calling assembly
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddServicedForCallingAssembly(this IServiceCollection services)
        {
            var callingAssembly = Assembly.GetCallingAssembly();

            return AddServiced(services, callingAssembly);
        }
        #endregion

        #region Registration
        private static void RegisterWithTypes(IServiceCollection services, Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            var descriptor = new ServiceDescriptor(serviceType, implementationType, lifetime);

            services.Add(descriptor);
        }

        private static void RegisterWithImplementationFactory(IServiceCollection services, Type implementationType, ServiceLifetime lifetime)
        {
            var classInstance = Activator.CreateInstance(implementationType);
            var factory = (Func<IServiceProvider, object>)implementationType.GetMethod(nameof(IHasImplementationFactory.GetFactory)).Invoke(classInstance, null);
            var descriptor = new ServiceDescriptor(implementationType, factory, lifetime);

            services.Add(descriptor);
        }

        #endregion Registration

        #region Helpers
        private static (Type serviceType, Type implementationType) GetTypes(Type serviceToRegister)
        {
            var genericInterface = serviceToRegister
                .GetInterfaces()
                .FirstOrDefault(x => x.IsGenericType && typeof(IServiced).IsAssignableFrom(x));

            return (genericInterface != null
                ? genericInterface.GetGenericArguments()[0]
                : serviceToRegister, serviceToRegister);
        }

        private static ServiceLifetime GetLifetime(Type serviceToRegister)
        {
            var lifetime = ServiceLifetime.Transient;

            if (typeof(IScoped).IsAssignableFrom(serviceToRegister))
            {
                lifetime = ServiceLifetime.Scoped;
            }
            else if (typeof(ISingleton).IsAssignableFrom(serviceToRegister))
            {
                lifetime = ServiceLifetime.Singleton;
            }

            return lifetime;
        }

        private static IEnumerable<Assembly> FilterAssemblies(params Assembly[] assemblies)
        {
            var currentAssembly = Assembly.GetAssembly(typeof(IServiced));

            return assemblies.Where(x => x.FullName != currentAssembly.FullName);
        }

        #endregion Helpers
    }
}