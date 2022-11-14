
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using System.Diagnostics.SymbolStore;


namespace PuzzleSolvers.Nanogram {
    internal class PuzzleSolver : IPuzzleSolver {
        public event EventHandler<ISolution> SolutionFound;

        private ICellScorer _cellScorer;
        private ILineSolver _lineSolver;

        private IPuzzle _puzzle;
        private CancellationToken _token;

        private ILinesHeap _linesHeap;
        private int[][] _gridRows;
        private int[][] _gridColumns;

        internal PuzzleSolver(ICellScorer cellScorer, ILineSolver lineSolver) {
            _cellScorer = cellScorer;
            _lineSolver = lineSolver;
        }

        #region SolveCommands
        public SolvingResult SolveForAnySolution(IPuzzle puzzle, out ISolution solution, CancellationToken token = new CancellationToken()) {
            PrepareForSolvingPuzzle(puzzle, token);

            var solvingResult = Solve(out List<ISolution> solutions, 1);
            solution = solvingResult != SolvingResult.Unsolvable ? solutions[0] : null;

            return solvingResult;
        }

        public SolvingResult SolveForManySolutions(IPuzzle puzzle, out List<ISolution> solutions, CancellationToken token = new CancellationToken(), int? limitNumberOfSolutions = null) {
            PrepareForSolvingPuzzle(puzzle, token);

            return Solve(out solutions, limitNumberOfSolutions);
        }
        public SolvingResult TrySolvingWithoutGuessing(IPuzzle puzzle, out ISolution partialSolution, CancellationToken token = new CancellationToken()) {
            PrepareForSolvingPuzzle(puzzle, token);

            var noGuessResult = TrySolvingNoGuess();
            partialSolution = noGuessResult != SolvingResult.Unsolvable ? GetCurrentSolution() : null;

            return noGuessResult;
        }

        public SolvingResult CompleteForAnySolutions(ISolution partialSolution, out ISolution solution, CancellationToken token = new CancellationToken()) {
            var result = CompleteSolution(partialSolution, null, out List<ISolution> solutions, token, 1);
            solution = result != SolvingResult.Unsolvable ? solutions[0] : null;

            return result;
        }
        public SolvingResult CompleteForAllSolutions(ISolution partialSolution, out List<ISolution> solutions, CancellationToken token = new CancellationToken(), int? limitNumberOfSolutions = null) {
            return CompleteSolution(partialSolution, null, out solutions, token, limitNumberOfSolutions);
        }

        internal SolvingResult CompleteSolution(ISolution partialSolution, Cell? guessedCell, out List<ISolution> solutions, CancellationToken token = new CancellationToken(), int? limitNumberOfSolutions = null) {
            PrepareForSolvingPuzzle(partialSolution.ParentPuzzle, token, partialSolution, guessedCell);

            return Solve(out solutions, limitNumberOfSolutions);
        }
        #endregion

        private void PrepareForSolvingPuzzle(IPuzzle puzzle, CancellationToken token, ISolution partialSolution = null, Cell? guessedCell = null) {
            _puzzle = puzzle;
            _token = token;

            if (partialSolution == null) {
                _gridRows = Enumerable.Range(0, _puzzle.Rows.Length).Select(x => Enumerable.Repeat(0, _puzzle.RowLength).ToArray()).ToArray();
                _gridColumns = Enumerable.Range(0, _puzzle.Columns.Length).Select(x => Enumerable.Repeat(0, _puzzle.ColumnLength).ToArray()).ToArray();
            } else {
                _gridRows = partialSolution.Grid.DeepCopy();
                _gridColumns = Enumerable.Range(0, _puzzle.Columns.Length).Select(x => partialSolution.GetGridColumn(x).DeepCopy()).ToArray();
            }

            if (guessedCell == null) {
                _linesHeap = Factory.CreateLinesHeap();
                _linesHeap.Populate(puzzle);

                _lineSolver.KeepTrackOfSolutions = false;
                _lineSolver.ClearCache();
            } else {
                _gridRows[guessedCell.Value.Row][guessedCell.Value.Column] = guessedCell.Value.Value;
                _gridColumns[guessedCell.Value.Column][guessedCell.Value.Row] = guessedCell.Value.Value;

                _linesHeap = Factory.CreateLinesHeap();
                _linesHeap.Add(_puzzle.Rows[guessedCell.Value.Row]);
                _linesHeap.Add(_puzzle.Columns[guessedCell.Value.Column]);
            }
        }

        private SolvingResult Solve(out List<ISolution> solutions, int? limitNumberOfSolutions = null) {
            solutions = null;
            var noGuessResult = TrySolvingNoGuess();
            if (noGuessResult == SolvingResult.Finished) {
                if (IsSolved()) {
                    var currentSolution = GetCurrentSolution();
                    solutions = new List<ISolution>() { GetCurrentSolution() };
                    SolutionFound?.Invoke(this, currentSolution);
                } else {
                    return SolveWithGuess(out solutions, limitNumberOfSolutions);
                }
            }
            return noGuessResult;
        }

        #region GuessSolVing
        private SolvingResult SolveWithGuess(out List<ISolution> solutions, int? limitNumberOfSolutions = null) {
            var cellToGuess = _cellScorer.GetHighestRatedUnsolvedCell(_puzzle, _gridRows);

            _lineSolver.KeepTrackOfSolutions = true;
            var partSolver = new PuzzleSolver(_cellScorer, _lineSolver);
            partSolver.SolutionFound = SolutionFound;
            solutions = new List<ISolution>();

            cellToGuess.Value = 1;
            var partResult = partSolver.CompleteSolution(GetCurrentSolution(), cellToGuess, out List<ISolution> partSolverSolutions, _token, limitNumberOfSolutions);

            if (partResult == SolvingResult.Finished) {
                solutions.AddRange(partSolverSolutions);
            }

            if (partResult != SolvingResult.Cancelled && (partResult == SolvingResult.Unsolvable || !LimitReached(solutions, limitNumberOfSolutions))) {
                cellToGuess.Value = -1;
                var part2Result = partSolver.CompleteSolution(GetCurrentSolution(), cellToGuess, out List<ISolution> part2SolverSolutions,
                    _token, partResult == SolvingResult.Finished && limitNumberOfSolutions.HasValue ?
                    limitNumberOfSolutions - partSolverSolutions.Count : limitNumberOfSolutions);

                if (part2Result == SolvingResult.Finished) {
                    solutions.AddRange(part2SolverSolutions);
                }
                if (partResult == SolvingResult.Finished && part2Result == SolvingResult.Unsolvable) {
                    return partResult;
                }

                return part2Result;
            }


            return partResult;
        }

        private bool LimitReached(List<ISolution> solutions, int? limitNumberOfSolutions) {
            if (limitNumberOfSolutions.HasValue && solutions.Count >= limitNumberOfSolutions.Value) {
                return true;
            } else {
                return false;
            }
        }
        #endregion

        #region NoGuessSolving
        private SolvingResult TrySolvingNoGuess() {
            return TrySolvingWithLineSolver();
        }
        private SolvingResult TrySolvingWithLineSolver() {
            while (!_linesHeap.IsEmpty()) {
                //Check for cancellation
                if (_token.IsCancellationRequested) {
                    return SolvingResult.Cancelled;
                }

                var lineToSolve = _linesHeap.GetKeyWithMaxValue();
                _linesHeap.Remove(lineToSolve);

                try {
                    //Solve line
                    var gridLine = lineToSolve.Orientation == LineOrientation.Row ? _gridRows[lineToSolve.Index] : _gridColumns[lineToSolve.Index];
                    var cellsToUpdate = _lineSolver.SolveForNewCells(lineToSolve, gridLine);

                    foreach (var cell in cellsToUpdate) {
                        //Update both grids
                        gridLine[cell.Index] = cell.Value;
                        (lineToSolve.Orientation == LineOrientation.Row ? _gridColumns : _gridRows)[cell.Index][lineToSolve.Index] = cell.Value;
                        //Incerement parallel line priority
                        _linesHeap.Increment(lineToSolve.Orientation == LineOrientation.Column ? _puzzle.Rows[cell.Index] : _puzzle.Columns[cell.Index]);
                    }
                } catch (LineException ex) {
                    return SolvingResult.Unsolvable;
                }
            }

            return SolvingResult.Finished;
        }
        #endregion

        #region HelperMethods

        private ISolution GetCurrentSolution() {
            return Factory.CreateSolution(_puzzle, _gridRows);
        }
        private bool IsSolved() {
            foreach (var gridRow in _gridRows) {
                foreach (var cell in gridRow) {
                    if (cell == 0) {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion
    }
}
