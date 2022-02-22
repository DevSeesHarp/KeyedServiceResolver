using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Devseesharp.KeyedServices.Extensions.Microsoft.DependencyInjection;

namespace Devseesharp.KeyedServices.Extensions.Microsoft.DependencyInjection.Test
{
    public class ServiceCollectionIntegrationTest
    {
        public enum FakeEnumKeys
        {
            Key1,
            Key2,
            Key3
        }
        interface ITestService { }

        [Fact]
        public void AddingKeyedServiceWithFactoryMustResolveCorrectService()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddTransient<TestService1>();
            services.AddTransient<TestService2>();

            services.AddKeyedServiceFactory<FakeEnumKeys, ITestService>(provider => key =>
            {
                return key switch
                {
                    FakeEnumKeys.Key1 => provider.GetRequiredService<TestService1>() as ITestService,
                    FakeEnumKeys.Key2 => provider.GetRequiredService<TestService2>(),
                    _ => throw new KeyNotFoundException()
                };
            }, ServiceLifetime.Scoped);
            var serviceProvider = services.BuildServiceProvider();

            // Act
            var keyedService = serviceProvider.GetService<IKeyedServiceResolver<FakeEnumKeys, ITestService>>();

            // Assert
            Assert.NotNull(keyedService);
            Assert.IsType<TestService1>(keyedService!.Get(FakeEnumKeys.Key1));
            Assert.IsType<TestService2>(keyedService.Get(FakeEnumKeys.Key2));
            Assert.ThrowsAny<KeyNotFoundException>(() => keyedService.Get(FakeEnumKeys.Key3));

        }


       

        [Fact]
        public void UsingServiceCollectionKeyedServicesMustReturnConfiguredServiceByKey()
        {
            // Arrange
            var services = new ServiceCollection();



            services.AddTransient<TestService1>();
            services.AddTransient<TestService2>();


            _ = services.AddTransient<KeyedServiceResolver<FakeEnumKeys, ITestService>.ResolverDelegate>(
                provider => key =>
                {
                    return key switch
                    {
                        FakeEnumKeys.Key1 => provider.GetRequiredService<TestService1>() as ITestService,
                        FakeEnumKeys.Key2 => provider.GetRequiredService<TestService2>(),
                        _ => throw new KeyNotFoundException()
                    };
                });
            services.AddTransient<KeyedServiceResolver<FakeEnumKeys, ITestService>>();






            var serviceProvider = services.BuildServiceProvider();

            var keyedService = serviceProvider.GetService<KeyedServiceResolver<FakeEnumKeys, ITestService>>();



            Assert.NotNull(keyedService);
            Assert.IsType<TestService1>(keyedService!.Get(FakeEnumKeys.Key1));
            Assert.IsType<TestService2>(keyedService.Get(FakeEnumKeys.Key2));
            Assert.ThrowsAny<KeyNotFoundException>(() => keyedService.Get(FakeEnumKeys.Key3));
        }
        class TestService1 : ITestService { }
        class TestService2 : ITestService { }
    }
}