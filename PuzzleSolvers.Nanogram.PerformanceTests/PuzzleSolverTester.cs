using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using PuzzleSolvers.Nanogram.Tests;
using PuzzleSolvers.Nonogram.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolvers.Nanogram.PerformanceTests {

    [MinColumn, MaxColumn, MeanColumn]
    [SimpleJob(RunStrategy.Monitoring, launchCount: 1, targetCount: 1)]
    [StopOnFirstError]
    public class PuzzleSolverTester {
        IPuzzleSolver _puzzleSolver;
        Dictionary<string, IPuzzle> _puzzles;

        [GlobalSetup]
        public void PuzzleSetup() {
            IPuzzle parsedPuzzle;
            _puzzles = new Dictionary<string, IPuzzle>();
            var fileParse = Factory.CreateFileParser();


            // yield return new object[] { TestFilesXML.webpbn034024 };    //ABOUT <100ms FOR ANY/MULTI
            // yield return new object[] { TestFilesXML.webpbn030110 }; //ABOUT 50ms FOR ANY/XXX FOR MULTI

            // yield return new object[] {TestFilesXML.webpbn008098 };//DOOM - ABOUT 16s FOR ANY/MULTI 
            //yield return new object[] { TestFilesXML.webpbn010810 };  //CENTER - ABOUT 200ms FOR ANY/XXX FOR MULTI


            fileParse.ParseXMLFileContent(TestFilesXML.webpbn034024, out parsedPuzzle, out _); //ABOUT <100ms FOR ANY/MULTI
            _puzzles.Add("webpbn034024", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn030110, out parsedPuzzle, out _); //ABOUT 50ms FOR ANY/XXX FOR MULTI
            _puzzles.Add("webpbn030110", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn008098, out parsedPuzzle, out _); //DOOM - ABOUT 16s FOR ANY/MULTI 
            _puzzles.Add("webpbn008098", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn010810, out parsedPuzzle, out _); //CENTER - ABOUT 200ms FOR ANY/XXX FOR MULTI
            _puzzles.Add("webpbn010810", parsedPuzzle);
        }


        [IterationSetup]
        public void SolverReset() {
            _puzzleSolver = Factory.CreatePuzzleSolver();
        }

        [Benchmark]
        [Arguments("webpbn034024")]
        [Arguments("webpbn008098")]
        [Arguments("webpbn030110")]
        [Arguments("webpbn010810")]
        public SolvingResult SingleSolution(string puzzlName) {
            return _puzzleSolver.SolveForAnySolution(_puzzles[puzzlName], out _);
        }
        [Benchmark]
        [Arguments("webpbn034024")]
        [Arguments("webpbn008098")]
        [Arguments("webpbn030110")]
        [Arguments("webpbn010810")]
        public SolvingResult MultiSolution(string puzzlName) {
            return _puzzleSolver.SolveForManySolutions(_puzzles[puzzlName], out _);
        }

    }

}
