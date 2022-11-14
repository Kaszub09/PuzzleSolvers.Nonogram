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
    [MemoryDiagnoser]
    [SimpleJob(RunStrategy.Monitoring, launchCount: 1, targetCount: 1)]
    public class PerformanceTestBatch {
        const int MaxTestTimeInMilisecs = 5000;
        IPuzzleSolver _puzzleSolver;
        Dictionary<string, IPuzzle> _puzzles;

        [GlobalSetup]
        public void PuzzleSetup() {
            IPuzzle parsedPuzzle;
            _puzzles = new Dictionary<string, IPuzzle>();
            var fileParse = Factory.CreateFileParser();

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn000001, out parsedPuzzle, out _);
            _puzzles.Add("webpbn000001", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn000006, out parsedPuzzle, out _);
            _puzzles.Add("webpbn000006", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn000016, out parsedPuzzle, out _);
            _puzzles.Add("webpbn000016", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn000021, out parsedPuzzle, out _);
            _puzzles.Add("webpbn000021", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn000023, out parsedPuzzle, out _);
            _puzzles.Add("webpbn000023", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn000027, out parsedPuzzle, out _);
            _puzzles.Add("webpbn000027", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn000065, out parsedPuzzle, out _);
            _puzzles.Add("webpbn000065", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn000436, out parsedPuzzle, out _);
            _puzzles.Add("webpbn000436", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn000529, out parsedPuzzle, out _);
            _puzzles.Add("webpbn000529", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn000803, out parsedPuzzle, out _);
            _puzzles.Add("webpbn000803", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn001611, out parsedPuzzle, out _);
            _puzzles.Add("webpbn001611", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn001694, out parsedPuzzle, out _);
            _puzzles.Add("webpbn001694", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn002040, out parsedPuzzle, out _);
            _puzzles.Add("webpbn002040", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn002413, out parsedPuzzle, out _);
            _puzzles.Add("webpbn002413", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn002556, out parsedPuzzle, out _);
            _puzzles.Add("webpbn002556", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn002712, out parsedPuzzle, out _);
            _puzzles.Add("webpbn002712", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn003541, out parsedPuzzle, out _);
            _puzzles.Add("webpbn003541", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn004645, out parsedPuzzle, out _);
            _puzzles.Add("webpbn004645", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn006574, out parsedPuzzle, out _);
            _puzzles.Add("webpbn006574", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn006739, out parsedPuzzle, out _);
            _puzzles.Add("webpbn006739", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn007604, out parsedPuzzle, out _);
            _puzzles.Add("webpbn007604", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn008098, out parsedPuzzle, out _);
            _puzzles.Add("webpbn008098", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn010088, out parsedPuzzle, out _);
            _puzzles.Add("webpbn010088", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn010810, out parsedPuzzle, out _);
            _puzzles.Add("webpbn010810", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn012548, out parsedPuzzle, out _);
            _puzzles.Add("webpbn012548", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn009892, out parsedPuzzle, out _);
            _puzzles.Add("webpbn009892", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn018297, out parsedPuzzle, out _);
            _puzzles.Add("webpbn018297", parsedPuzzle);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn022336, out parsedPuzzle, out _);
            _puzzles.Add("webpbn022336", parsedPuzzle);
        }


        [IterationSetup]
        public void SolverReset() {
            _puzzleSolver = Factory.CreatePuzzleSolver();
        }

        [Benchmark]
        [Arguments("webpbn000001", "#1: Dancer*")]
        [Arguments("webpbn000006", "#6: Cat*")]
        [Arguments("webpbn000021", "#21: Skid*")]
        [Arguments("webpbn000027", "#27: Bucks*")]
        [Arguments("webpbn000023", "#23: Edge*")]
        [Arguments("webpbn002413", "#2413: Smoke")]
        [Arguments("webpbn000016", "#16: Knot*")]
        [Arguments("webpbn000529", "#529: Swing*")]
        [Arguments("webpbn000065", "#65: Mum*")]
        [Arguments("webpbn007604", "#7604: DiCap")]
        [Arguments("webpbn001694", "#1694: Tragic")]
        [Arguments("webpbn001611", "#1611: Merka*")]
        [Arguments("webpbn000436", "#436: Petro*")]
        [Arguments("webpbn004645", "#4645: M&M")]
        [Arguments("webpbn003541", "#3541: Signed")]
        [Arguments("webpbn000803", "#803: Light*")]
        [Arguments("webpbn006574", "#6574: Forever*")]
        [Arguments("webpbn010810", "#10810: Center")]
        [Arguments("webpbn002040", "#2040: Hot")]
        [Arguments("webpbn006739", "#6739: Karate")]
        [Arguments("webpbn008098", "#8098: 9-Dom*")]
        [Arguments("webpbn002556", "#2556: Flag")]
        [Arguments("webpbn002712", "#2712: Lion")]
        [Arguments("webpbn010088", "#10088: Marley")]
        [Arguments("webpbn018297", "#18297: Thing")]
        [Arguments("webpbn009892", "#9892: Nature")]
        [Arguments("webpbn012548", "#12548: Sierp")]
        [Arguments("webpbn022336", "#22336: Gettys")]

        public bool AnySolution(string puzzleId, string puzzleDescription) {
            var t = new Task(() => Factory.CreatePuzzleSolver().SolveForAnySolution(_puzzles[puzzleId], out _));
            t.Start();
            if (t.Wait(MaxTestTimeInMilisecs)) {
                return true;
            } else {
                throw new Exception("Time limit exceeded");
            }
        }

        [Benchmark]
        [Arguments("webpbn000001", "#1: Dancer*")]
        [Arguments("webpbn000006", "#6: Cat*")]
        [Arguments("webpbn000021", "#21: Skid*")]
        [Arguments("webpbn000027", "#27: Bucks*")]
        [Arguments("webpbn000023", "#23: Edge*")]
        [Arguments("webpbn002413", "#2413: Smoke")]
        [Arguments("webpbn000016", "#16: Knot*")]
        [Arguments("webpbn000529", "#529: Swing*")]
        [Arguments("webpbn000065", "#65: Mum*")]
        [Arguments("webpbn007604", "#7604: DiCap")]
        [Arguments("webpbn001694", "#1694: Tragic")]
        [Arguments("webpbn001611", "#1611: Merka*")]
        [Arguments("webpbn000436", "#436: Petro*")]
        [Arguments("webpbn004645", "#4645: M&M")]
        [Arguments("webpbn003541", "#3541: Signed")]
        [Arguments("webpbn000803", "#803: Light*")]
        [Arguments("webpbn006574", "#6574: Forever*")]
        [Arguments("webpbn010810", "#10810: Center")]
        [Arguments("webpbn002040", "#2040: Hot")]
        [Arguments("webpbn006739", "#6739: Karate")]
        [Arguments("webpbn008098", "#8098: 9-Dom*")]
        [Arguments("webpbn002556", "#2556: Flag")]
        [Arguments("webpbn002712", "#2712: Lion")]
        [Arguments("webpbn010088", "#10088: Marley")]
        [Arguments("webpbn018297", "#18297: Thing")]
        [Arguments("webpbn009892", "#9892: Nature")]
        [Arguments("webpbn012548", "#12548: Sierp")]
        [Arguments("webpbn022336", "#22336: Gettys")]
        public bool AnySolutionUniqueCheck(string puzzleId, string puzzleDescription) {
            var t = new Task(() => Factory.CreatePuzzleSolver().SolveForManySolutions(_puzzles[puzzleId], out _, default, 2));;
            t.Start();
            if (t.Wait(MaxTestTimeInMilisecs)) {
                return true;
            } else {
                throw new Exception("Time limit exceeded");
            }
        }
    }
}

