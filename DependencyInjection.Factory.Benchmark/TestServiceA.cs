namespace DependencyInjection.Benchmark;

public class TestServiceA : ITestService
{
    public void DoSomething()
    {
        Console.WriteLine("TestServiceA did something");
    }
}
