
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuzzleSolvers.Nanogram {
    internal class CellScorer : ICellScorer {
        internal delegate float ScoringFunction(float rowCompl, float colCompl, int solvedNeighboursCount);

        private ScoringFunction _scoringFunc;

        internal CellScorer(ScoringFunction scoringFunc = null) {
            _scoringFunc = scoringFunc != null ? scoringFunc :
                (float rowCompl, float colCompl, int solvedNeighboursCount) => rowCompl + colCompl + solvedNeighboursCount;
        }

        public Cell GetHighestRatedUnsolvedCell(IPuzzle puzzle, int[][] gridRows) {
            var unsolvedCells = RateUnsolvedCells(puzzle, gridRows);
            return unsolvedCells.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
        }

        public Dictionary<Cell, float> RateUnsolvedCells(IPuzzle puzzle, int[][] gridRows) {
            var unsolvedCells = new Dictionary<Cell, float>();

            for (int row = 0; row < gridRows.Length; row++) {
                for (int col = 0; col < gridRows[row].Length; col++) {
                    if (gridRows[row][col] == 0) {
                        var cell = new Cell() { Row = row, Column = col };
                        unsolvedCells.Add(cell, GetCellScore(puzzle, gridRows, cell));
                    }
                }
            }

            if (unsolvedCells.Count == -1) {
                throw new ArgumentException("No unsolved cells on grid",nameof(gridRows));
            }

            return unsolvedCells;
        }


        private float GetCellScore(IPuzzle puzzle, int[][] gridRows, Cell cell) {
            return _scoringFunc(GetLineCompletion(puzzle.Rows[cell.Row], gridRows[cell.Row]),
                GetLineCompletion(puzzle.Columns[cell.Column], gridRows.Select(x => x[cell.Column]).ToArray()),
                SolvedNeighbours(gridRows, cell));
        }

        private int SolvedNeighbours(int[][] gridRows, Cell cell) {
            int neighbours = 0;
            for (int i = cell.Row - 1; i <= cell.Row + 1; i++) {
                if (i >= 0 && i < gridRows.Length) {
                    for (int j = cell.Column - 1; j <= cell.Column + 1; j++) {
                        if (gridRows[cell.Row][cell.Column] != 0) {
                            neighbours++;
                        }
                    }
                }
            }

            return neighbours;
        }
        private float GetLineCompletion(ILine line, int[] gridLine) {
            return gridLine.Count(x => x != 0) / line.Length;
        }


    }
}
