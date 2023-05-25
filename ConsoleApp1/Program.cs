using DependencyInjection.Benchmark;
using DependencyInjection.Factory;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;


namespace DependencyInjection.Benchmark;

public class FactoryComparison
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IFactory<ITestService> _factory;

    public FactoryComparison()
    {
        // Initialize the service collection and the factory
        var services = new ServiceCollection();
        services.InitializeFactory();
        services.AddFactory<ITestService, TestServiceA>(FactoryScope.Transient);
        services.AddFactory<ITestService, TestServiceB>(FactoryScope.Transient);
        _serviceProvider = services.BuildServiceProvider();
        _factory = _serviceProvider.GetRequiredService<IFactory<ITestService>>();
    }

    [Benchmark]
    public void CreateTestServiceAWithFactory()
    {
        // Create an instance of TestServiceA using the factory
        var service = _factory.Create<TestServiceA>();
        service.DoSomething();
    }

    [Benchmark]
    public void CreateTestServiceBWithFactory()
    {
        // Create an instance of TestServiceB using the factory
        var service = _factory.Create<TestServiceB>();
        service.DoSomething();
    }

    [Benchmark]
    public void CreateTestServiceAWithServiceProvider()
    {
        // Create an instance of TestServiceA using the service provider
        var service = _serviceProvider.GetRequiredService<TestServiceA>();
        service.DoSomething();
    }

    [Benchmark]
    public void CreateTestServiceBWithServiceProvider()
    {
        // Create an instance of TestServiceB using the service provider
        var service = _serviceProvider.GetRequiredService<TestServiceB>();
        service.DoSomething();
    }
}
