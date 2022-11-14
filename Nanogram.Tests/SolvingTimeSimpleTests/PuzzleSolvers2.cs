using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace PuzzleSolvers.Nanogram.Tests.PerformanceTests {
    public class PuzzleSolvers2 {
        string outputFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+@"\NANOGRAM_SOLVER_2";
        Dictionary<string, IPuzzle> _puzzles;
        Dictionary<string, List<ISolution>> _solutions;
        public PuzzleSolvers2() {
            Directory.CreateDirectory(outputFolderPath);

            IPuzzle parsedPuzzle;
            List<ISolution> parsedSolutions;
            _puzzles = new Dictionary<string, IPuzzle>();
            _solutions = new Dictionary<string, List<ISolution>>();
            var fileParse = Factory.CreateFileParser();

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn000001, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn000001", parsedPuzzle);
            _solutions.Add("webpbn000001", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn000006, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn000006", parsedPuzzle);
            _solutions.Add("webpbn000006", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn000016, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn000016", parsedPuzzle);
            _solutions.Add("webpbn000016", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn000021, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn000021", parsedPuzzle);
            _solutions.Add("webpbn000021", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn000023, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn000023", parsedPuzzle);
            _solutions.Add("webpbn000023", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn000027, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn000027", parsedPuzzle);
            _solutions.Add("webpbn000027", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn000065, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn000065", parsedPuzzle);
            _solutions.Add("webpbn000065", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn000436, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn000436", parsedPuzzle);
            _solutions.Add("webpbn000436", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn000529, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn000529", parsedPuzzle);
            _solutions.Add("webpbn000529", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn000803, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn000803", parsedPuzzle);
            _solutions.Add("webpbn000803", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn001611, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn001611", parsedPuzzle);
            _solutions.Add("webpbn001611", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn001694, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn001694", parsedPuzzle);
            _solutions.Add("webpbn001694", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn002040, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn002040", parsedPuzzle);
            _solutions.Add("webpbn002040", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn002413, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn002413", parsedPuzzle);
            _solutions.Add("webpbn002413", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn002556, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn002556", parsedPuzzle);
            _solutions.Add("webpbn002556", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn002712, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn002712", parsedPuzzle);
            _solutions.Add("webpbn002712", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn003541, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn003541", parsedPuzzle);
            _solutions.Add("webpbn003541", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn004645, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn004645", parsedPuzzle);
            _solutions.Add("webpbn004645", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn006574, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn006574", parsedPuzzle);
            _solutions.Add("webpbn006574", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn006739, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn006739", parsedPuzzle);
            _solutions.Add("webpbn006739", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn007604, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn007604", parsedPuzzle);
            _solutions.Add("webpbn007604", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn008098, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn008098", parsedPuzzle);
            _solutions.Add("webpbn008098", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn010088, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn010088", parsedPuzzle);
            _solutions.Add("webpbn010088", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn010810, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn010810", parsedPuzzle);
            _solutions.Add("webpbn010810", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn012548, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn012548", parsedPuzzle);
            _solutions.Add("webpbn012548", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn009892, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn009892", parsedPuzzle);
            _solutions.Add("webpbn009892", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn018297, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn018297", parsedPuzzle);
            _solutions.Add("webpbn018297", parsedSolutions);

            fileParse.ParseXMLFileContent(TestFilesXML.webpbn022336, out parsedPuzzle, out parsedSolutions);
            _puzzles.Add("webpbn022336", parsedPuzzle);
            _solutions.Add("webpbn022336", parsedSolutions);
        }

        [Theory]
        [InlineData("webpbn008098", "#8098: 9-Dom*")]
        internal void SolveForAny(string puzzleId, string puzzleDescription) {
            var file = outputFolderPath + @$"\{puzzleId}_{puzzleDescription}_resAny.txt";

            var puzzleSolver = Factory.CreatePuzzleSolver2();
            puzzleSolver.MaxSolutionsCount = 1;
            var res = puzzleSolver.Solve(_puzzles[puzzleId], out List<ISolution> solution);

            File.WriteAllLines(file, solution[0].Grid.GetAsString().Split('\n'));
            Assert.Equal(SolvingResult.Finished,res);
        }

        [Theory]
        [InlineData("webpbn008098", "#8098: 9-Dom*")]
        internal void SolveForAll(string puzzleId, string puzzleDescription) {
            var file = outputFolderPath + @$"\{puzzleId}_{puzzleDescription}_resMulti.txt";

            var puzzleSolver = Factory.CreatePuzzleSolver2();

            var res = puzzleSolver.Solve(_puzzles[puzzleId], out List<ISolution> solvedSolutions);


            if (File.Exists(file)) {
                File.Delete(file);
            }

            foreach (var sol in solvedSolutions) {
                File.AppendAllLines(file, sol.Grid.GetAsString().Split('\n'));
                File.AppendAllLines(file, new string[] { "\n\n" });
            }
            Assert.NotEmpty(solvedSolutions.Union(_solutions[puzzleId]));
            Assert.Equal(SolvingResult.Finished, res);
        }


    }
}
