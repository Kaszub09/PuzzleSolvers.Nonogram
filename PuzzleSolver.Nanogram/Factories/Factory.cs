
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PuzzleSolvers.Nanogram;

namespace PuzzleSolvers.Nanogram {
    public static class Factory {

        public static ILine CreateLine(LineOrientation orientation, int index, int[] groups, int length) {
            return new Line(orientation, index, groups, length);
        }

        public static IPuzzle CreatePuzzle(int[][] rows, int[][] columns) {
            return new Puzzle(rows.Select((row, idx) => CreateLine(LineOrientation.Row, idx,row, columns.Length)).ToArray(),
                columns.Select((column,idx) => CreateLine(LineOrientation.Column, idx,column, rows.Length )).ToArray());
        }

        public static ISolution CreateSolution(IPuzzle parentPuzzle, int[][] gridRowCol) {
            return new Solution(parentPuzzle, gridRowCol);
        }

        public static IPuzzleSolver CreatePuzzleSolver() {
            return new PuzzleSolver(CreateCellScorer(), CreateLineSolver());
        }

        public static IFileParser CreateFileParser() {
            return new FileParser();
        }

        internal static PuzzleSolver2 CreatePuzzleSolver2() {
            return new PuzzleSolver2(CreateCellScorer());
        }
        internal static IPuzzleSolver CreatePuzzleSolver(CellScorer.ScoringFunction scoringFunc) {
            return new PuzzleSolver(CreateCellScorer(scoringFunc), CreateLineSolver());
        }

        internal static ICellScorer CreateCellScorer(CellScorer.ScoringFunction scoringFunc = null) {
            return new CellScorer(scoringFunc);
        }

        internal static ILineSolver CreateLineSolver() {
            return new LeftRightLineSolver();
        }

        internal static ILinesHeap CreateLinesHeap() {
            return new LinesHeap();
        }



    }
}
