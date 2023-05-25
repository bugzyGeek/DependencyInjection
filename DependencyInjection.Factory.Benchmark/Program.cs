using BenchmarkDotNet.Running;
using DependencyInjection.Benchmark;


// A main method to run the benchmark test
var summary = BenchmarkRunner.Run<FactoryBenchmark>();
