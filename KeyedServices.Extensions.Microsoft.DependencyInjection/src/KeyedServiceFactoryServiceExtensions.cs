using Microsoft.Extensions.DependencyInjection;
using System;

namespace Devseesharp.KeyedServices.Extensions.Microsoft.DependencyInjection
{
    public static class KeyedServiceFactoryServiceExtensions
    {
        /// <summary>
        /// Registers service factory for <typeparamref name="TService"/> and storing the factory in a delegate.
        /// <para/>
        /// Get <typeparamref name="TService"/> service by passing in a <typeparamref name="TKey"/> <paramref name="key"/> into the factory.
        /// Both the <see cref="KeyedServiceResolver{TKey, TService}.ResolverDelegate"/> and the <see cref="KeyedServiceResolver{TKey, TService}"/> need to be registered in the DI container.
        /// <see cref="IKeyedServiceResolver{TKey, TService}"/> should be used for resolving this service.
        /// See more examples in readme or unit-tests.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TService"></typeparam>
        public static void AddKeyedServiceFactory<TKey, TService>(this IServiceCollection services,
            Func<IServiceProvider, KeyedServiceResolver<TKey, TService>.ResolverDelegate> implementationFactory, ServiceLifetime lifetime)
        {
            services.Add(new ServiceDescriptor(typeof(KeyedServiceResolver<TKey, TService>.ResolverDelegate), implementationFactory, lifetime));
            services.Add(new ServiceDescriptor(typeof(IKeyedServiceResolver<TKey, TService>), typeof(KeyedServiceResolver<TKey, TService>), lifetime));
        }

    }
}