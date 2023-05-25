using System;

namespace DependencyInjection.Factory.UnitTest;
public class TestServiceA : ITestService
{
    public void DoSomething()
    {
        Console.WriteLine("TestServiceA did something");
    }
}
