using System;

namespace DependencyInjection.Factory.UnitTest;

public class TestServiceB : ITestService
{
    public void DoSomething()
    {
        Console.WriteLine("TestServiceB did something");
    }
}
