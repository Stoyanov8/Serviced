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
        public static IServiceCollection AddServiced(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            assemblies = FilterAssemblies(assemblies);

            var servicesToRegister = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(t => (typeof(IServiced).IsAssignableFrom(t)))
                .ToList();

            foreach (var serviceToRegister in servicesToRegister)
            {
                var (serviceType, implementationType) = GetTypes(serviceToRegister);

                var lifetime = GetLifetime(serviceToRegister);

                if (typeof(IHasImplementationFactory).IsAssignableFrom(serviceToRegister))
                {
                    RegisterWithImplementationFactory(services, serviceToRegister, lifetime);
                }
                else
                {
                    RegisterWithTypes(services, serviceType, implementationType, lifetime);
                }
            }

            return services;
        }

        public static IServiceCollection AddServiced(this IServiceCollection services, Assembly assembly)
            => AddServiced(services, new List<Assembly> { assembly });

        #endregion

        #region Registration
        private static void RegisterWithTypes(IServiceCollection services, Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            var descriptor = new ServiceDescriptor(serviceType, implementationType, lifetime);

            services.Add(descriptor);
        }

        private static void RegisterWithImplementationFactory(IServiceCollection services, Type serviceToRegister, ServiceLifetime lifetime)
        {
            var classInstance = Activator.CreateInstance(serviceToRegister);
            var factory = (Func<IServiceProvider, object>)serviceToRegister.GetMethod(nameof(IHasImplementationFactory.GetFactory)).Invoke(classInstance, null);
            var descriptor = new ServiceDescriptor(serviceToRegister, factory, lifetime);

            services.Add(descriptor);
        }

        #endregion Registration

        #region Helpers
        private static (Type serviceType, Type implementationType) GetTypes(Type serviceToRegister)
        {
            var genericInterface = serviceToRegister
                .GetInterfaces()
                .FirstOrDefault(x => x.IsGenericType && typeof(IServiced).IsAssignableFrom(x));

            return (genericInterface != null ? genericInterface.GetGenericArguments()[0] : null, serviceToRegister);
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

        private static IEnumerable<Assembly> FilterAssemblies(IEnumerable<Assembly> assemblies)
        {
            var currentAssembly = Assembly.GetAssembly(typeof(IServiced));

            return assemblies
                .Where(x => x.FullName != currentAssembly.FullName);
        }

        #endregion Helpers
    }
}