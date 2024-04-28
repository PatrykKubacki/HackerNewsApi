using BenchmarkDotNet.Running;
using HackerNewsTests;

var results = BenchmarkRunner.Run<Demo>();
Console.ReadKey();