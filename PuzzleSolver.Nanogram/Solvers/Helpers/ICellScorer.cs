
using System.Collections.Generic;

namespace PuzzleSolvers.Nanogram {
    internal interface ICellScorer {
        Cell GetHighestRatedUnsolvedCell(IPuzzle puzzle, int[][] gridRows);
        Dictionary<Cell, float> RateUnsolvedCells(IPuzzle puzzle, int[][] gridRows);
    }
}