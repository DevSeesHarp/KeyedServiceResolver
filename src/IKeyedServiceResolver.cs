namespace Devseesharp.KeyedServices
{
    /// <summary>
    /// Wraps the delegate that returns the <typeparamref name="TService"/> by passing in a <typeparamref name="TKey"/> <paramref name="key"/>
    /// <para/>
    /// Don't forget to register type <see cref="KeyedServiceResolver{TKey, TService}"/> into your service resolver after registering the delegate!
    /// See more examples in readme or unit-tests.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TService"></typeparam>
    public interface IKeyedServiceResolver<in TKey, out TService>
    {
        /// <summary>
        /// Returns the <typeparamref name="TService"/> from <typeparamref name="TKey"/> <paramref name="key"/> using the configured delegate <see cref="ResolverDelegate"/>
        /// </summary>
        /// <param name="key"></param>
        /// <returns><typeparamref name="TService"/></returns>
        TService Get(TKey key);
    }
}