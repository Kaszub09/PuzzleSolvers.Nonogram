
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PuzzleSolvers.Nanogram.Tests.FunctioningTests.Solvers {
    public class LineSolvers2 {
        [Theory]
        [MemberData(nameof(LineSolverData))]
        internal void LineSolving(int[] groups, int[] gridLine,bool isSolvable, List<LineCell> cellsToUpdate) {
            var rand = new Random();
            var line = Factory.CreateLine(rand.Next() % 2 == 0 ? LineOrientation.Row : LineOrientation.Column,
                rand.Next(0, int.MaxValue), groups, gridLine.Length);
            var lineSolver =new LeftRightLineSolver2();

            var solverResult = lineSolver.SolveForNewCells(line, gridLine);
            if (isSolvable) {
                Assert.Equal<LineCell>(cellsToUpdate, solverResult);
            } else {
                Assert.Null(solverResult);
            }
        }

        internal static IEnumerable<object[]> LineSolverData() {
            yield return new object[] { new int[] { },
                new int[] { 0, 0, 0},
                true,
                new List<LineCell>() {new LineCell(0,- 1) ,new LineCell(1, -1),new LineCell(2, -1) }};
            yield return new object[] { new int[] { 3},
                new int[] { 0, 0, 0},
                true,
                new List<LineCell>() {new LineCell(0, 1) ,new LineCell(1, 1),new LineCell(2, 1) }};
            yield return new object[] { new int[] { 3},
                new int[] { 0, 1,1},
                true,
                new List<LineCell>() {new LineCell(0, 1) } };
            yield return new object[] { new int[] { 2,2},
                new int[] { 0, 0,-1,0,-1,0,0,0},
                true,
                new List<LineCell>() { new LineCell(0, 1),new LineCell(1,1) ,new LineCell(3,-1), new LineCell(6, 1) } };
            yield return new object[] { new int[] { 2,2},
                new int[] { 0, -1,-1,-1,-1,0,0,0},
                false,
                new List<LineCell>() { } };

        }

    }
}
