namespace DependencyInjection.Benchmark;

public class TestServiceB : ITestService
{
    public void DoSomething()
    {
        Console.WriteLine("TestServiceB did something");
    }
}
