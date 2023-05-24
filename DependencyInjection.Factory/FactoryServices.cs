using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.Factory;

public static class FactoryServices
{
    public static IServiceCollection? ServiceDescriptors { get; private set; }

    /// <summary>
    /// Initialize a factory builder to a specific IServiceCollection
    /// </summary>
    /// <param name="serviceDescriptors">An IServiceCollection</param>
    /// <returns>A reference of this instance after the operation is completed</returns>
    public static IServiceCollection InitializeFactory(this IServiceCollection serviceDescriptors)
    {
        ServiceDescriptors ??= serviceDescriptors;
        return serviceDescriptors;
    }

    /// <summary>
    /// Add a service of type specified in <typeparamref name="TInterface"/> with the Implementation type specified in <typeparamref name="TImplementation"/> to the specified <typeparamref name="IServiceCollection"/>
    /// </summary>
    /// <typeparam name="TInterface">The type of service to add</typeparam>
    /// <typeparam name="TImplementation">The type of implementation to use</typeparam>
    /// <param name="services">The specificed IServiceCollection the factory is registered to</param>
    /// <param name="factoryScope">The type of service to register</param>
    /// <returns>A reference of this instance after the operation is completed</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddFactory<TInterface, TImplementation>(this IServiceCollection services, FactoryScope factoryScope)
where TInterface : class
where TImplementation : class, TInterface
    {
        if (services is null)
            throw new ArgumentNullException(nameof(services));

        switch (factoryScope)
        {
            case FactoryScope.Scope:
                services.AddScoped<TInterface, TImplementation>();
                break;
            case FactoryScope.Singleton:
                services.AddSingleton<TInterface, TImplementation>();
                break;
            case FactoryScope.Transient:
                services.AddTransient<TInterface, TImplementation>();
                break;
        }
        services.AddSingleton<IFactory<TInterface>, Factory<TInterface>>();
        services.AddSingleton<IFactory<TInterface>, FactoryFunc<TInterface>>();
        services.AddSingleton<Func<TInterface>>(x => () =>
        {
            try
            {
                return x.GetService<TInterface>()!;
            }
            catch (InvalidOperationException)
            {
                using IServiceScope scope = x.CreateScope();
                return scope.ServiceProvider.GetService<TInterface>()!;
            }
        });

        return services;
    }

    /// <summary>
    /// Add a service of type specified in <typeparamref name="TImplementation"/> to the specified <typeparamref name="IServiceCollection"/>
    /// </summary>
    /// <typeparam name="TImplementation">The type of implementation to use</typeparam>
    /// <param name="services">The specifice IServiceCollection the factory is being generated on</param>
    /// <param name="factoryScope">The type of service to register</param>
    /// <returns>A reference of this instance after the operation is completed</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddFactory<TImplementation>(this IServiceCollection services, FactoryScope factoryScope)
        where TImplementation : class
    {
        if (services is null)
            throw new ArgumentNullException(nameof(services));

        switch (factoryScope)
        {
            case FactoryScope.Scope:
                services.AddScoped<TImplementation>();
                break;
            case FactoryScope.Singleton:
                services.AddSingleton<TImplementation>();
                break;
            case FactoryScope.Transient:
                services.AddTransient<TImplementation>();
                break;
        }
        services.AddSingleton<IFactory<TImplementation>, Factory<TImplementation>>();
        services.AddSingleton<IFactory<TImplementation>, FactoryFunc<TImplementation>>();
        services.AddSingleton<Func<TImplementation>>(x => () =>
        {
            try
            {
                return x.GetService<TImplementation>()!;
            }
            catch (InvalidOperationException)
            {
                using IServiceScope scope = x.CreateScope();
                return scope.ServiceProvider.GetService<TImplementation>()!;
            }
        });

        return services;
    }

}