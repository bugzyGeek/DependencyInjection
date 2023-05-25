// A benchmark class to measure the performance of the factory pattern
using DependencyInjection.Benchmark;
using DependencyInjection.Factory;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;


namespace DependencyInjection.Benchmark;

public class FactoryBenchmark
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IFactory<ITestService> _factory;

    public FactoryBenchmark()
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
    public void CreateTestServiceA()
    {
        // Create an instance of TestServiceA using the factory
        var service = _factory.Create<TestServiceA>();
        service.DoSomething();
    }

    [Benchmark]
    public void CreateTestServiceB()
    {
        // Create an instance of TestServiceB using the factory
        var service = _factory.Create<TestServiceB>();
        service.DoSomething();
    }
}