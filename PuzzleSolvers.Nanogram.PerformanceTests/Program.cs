using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Security.Cryptography;
using PuzzleSolvers.Nanogram.PerformanceTests;


//var summaryMulti = BenchmarkRunner.Run<PuzzleSolverMulti>();
//var summarySingle = BenchmarkRunner.Run<PerformanceTestBatch>();
 Dictionary<string, List<string>> _solveHistory = new Dictionary<string, List<string>>(1000000);
Console.Read();