## Keyed Service Resolver

A extension that injects a wrapper that can resolve services by key



Install directly with..
### Package Manager
Install-Package Devseesharp.KeyedServices.Extensions.Microsoft.DependencyInjection

### .NET CLI
dotnet add package Devseesharp.KeyedServices.Extensions.Microsoft.DependencyInjection

### Or visit nuget.org
[nuget.org/packages/Devseesharp.KeyedServices.Extensions.Microsoft.DependencyInjection](https://www.nuget.org/packages/Devseesharp.KeyedServices.Extensions.Microsoft.DependencyInjection)

### Features

- Resolve services by key
- Register services by key fast and easy
- Wide compatibility with .Net Standard 2.1


### Getting started

First register your services with scope of choice. 
Then you register the resolver IKeyedResolver<TKey,TService> using the AddKeyedServiceFactory() extension.

```csharp
using Devseesharp.KeyedServices.Extensions.Microsoft.DependencyInjection;

// #1
services.AddTransient<TestService1>();
services.AddTransient<TestService2>();

// #2
services.AddKeyedServiceFactory<FakeEnumKeys, ITestService>(provider => key =>
{
    return key switch
    {
        FakeEnumKeys.Key1 => provider.GetRequiredService<TestService1>() as ITestService,
        FakeEnumKeys.Key2 => provider.GetRequiredService<TestService2>(),
        _ => throw new KeyNotFoundException()
    };
}, ServiceLifetime.Scoped);

```


Later when you want to get the service by key, you inject the IKeyedServiceResolver<TKey,TService> into your object and call the method Get(TKey), that will then resolve your service.

```csharp

using Devseesharp.KeyedServices;

public class DevseesharpService
{
    private ITestService _fakeService2;

    public DevseesharpService(IKeyedServiceResolver<FakeEnumKeys, ITestService> keyedServiceResolver)
    {
       _fakeService2 = keyedServiceResolver.Get(FakeEnumKeys.Key2);
    }
}

```