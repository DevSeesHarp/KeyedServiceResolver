namespace Devseesharp.KeyedServices
{
    /// <summary>
    /// Wraps the delegate that returns the <typeparamref name="TService"/> by passing in a <typeparamref name="TKey"/> <paramref name="key"/>
    /// <para/>
    /// Both the <see cref="KeyedServiceResolver{TKey, TService}.ResolverDelegate"/> and the <see cref="KeyedServiceResolver{TKey, TService}"/> need to be registered in the DI container.
    /// <see cref="IKeyedServiceResolver{TKey, TService}"/> should be used for resolving this service.
    /// See more examples in readme or unit-tests.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TService"></typeparam>
    public class KeyedServiceResolver<TKey, TService> : IKeyedServiceResolver<TKey, TService>
    {
        protected readonly ResolverDelegate _resolverDelegate;
        public KeyedServiceResolver(ResolverDelegate resolverDelegate) 
            => _resolverDelegate = resolverDelegate;
        
        /// <summary>
        /// Delegate returns the <typeparamref name="TService"/> by passing in a <typeparamref name="TKey"/> <paramref name="key"/>
        /// </summary>
        /// <param name="key"></param>
        /// <returns><typeparamref name="TService"/></returns>
        public delegate TService ResolverDelegate(TKey key);

        /// <summary>
        /// Resolves <typeparamref name="TService"/> from the configured delegate <see cref="ResolverDelegate"/> using the argument <typeparamref name="TKey"/> <paramref name="key"/>
        /// </summary>
        /// <param name="key"></param>
        /// <returns><typeparamref name="TService"/></returns>
        public TService Get(TKey key)
        {
            var r = _resolverDelegate(key);
            return r;
        }
    }
}