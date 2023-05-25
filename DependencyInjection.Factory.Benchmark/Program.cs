using BenchmarkDotNet.Running;
using DependencyInjection.Benchmark;
using DependencyInjection.Factory.Benchmark;


// A main method to run the benchmark test
//var summary = BenchmarkRunner.Run<FactoryBenchmark>();
var summaryComparision = BenchmarkRunner.Run<FactoryComparison>();
