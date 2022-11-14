
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using System.Diagnostics.SymbolStore;


namespace PuzzleSolvers.Nanogram {
    internal class PuzzleSolver2 {
        public event EventHandler<ISolution> SolutionFound;

        private ICellScorer _cellScorer;
        private LeftRightLineSolver2 _lineSolver;

        private IPuzzle _puzzle;
        private CancellationToken _token;

        private ILinesHeap _linesHeap;
        private int[][] _gridRows;
        private int[][] _gridColumns;

        private List<ISolution> _solutions;
        private List<Cell> _guessHistory;
        private bool _isGuessing;

        public int MaxSolutionsCount { get; set; } = int.MaxValue;

        internal PuzzleSolver2(ICellScorer cellScorer) {
            _cellScorer = cellScorer;
            _lineSolver = new LeftRightLineSolver2();
        }

        #region SolveCommands


        public SolvingResult Solve(IPuzzle puzzle, out List<ISolution> solutions, CancellationToken token = new CancellationToken()) {
            PrepareForSolvingPuzzle(puzzle, token);
            solutions = _solutions;
            return  Solve();
        }

        #endregion

        private void PrepareForSolvingPuzzle(IPuzzle puzzle, CancellationToken token) {
            _puzzle = puzzle;
            _token = token;

            _linesHeap = Factory.CreateLinesHeap();
            _linesHeap.Populate(puzzle);

            _gridRows = Enumerable.Range(0, _puzzle.Rows.Length).Select(x => Enumerable.Repeat(0, _puzzle.RowLength).ToArray()).ToArray();
            _gridColumns = Enumerable.Range(0, _puzzle.Columns.Length).Select(x => Enumerable.Repeat(0, _puzzle.ColumnLength).ToArray()).ToArray();

            _isGuessing = false;
            _solutions = new List<ISolution>();
            _guessHistory = new List<Cell>();

            _lineSolver.KeepTrackOfSolutions = false;
            _lineSolver.ClearCache();
        }

        private SolvingResult Solve() {
            var noGuessResult = TrySolvingNoGuess();
            if (noGuessResult == SolvingResult.Finished) {
                if (IsSolved()) {
                    var sol = GetCurrentSolution();
                    _solutions.Add(sol);
                    SolutionFound?.Invoke(this, sol);
                } else {
                    return SolveWithGuess();
                }
            }
            return noGuessResult;
        }

        #region GuessSolVing
        private SolvingResult SolveWithGuess() {
            _lineSolver.KeepTrackOfSolutions = true;
            var unsolvedCells = _cellScorer.RateUnsolvedCells(_puzzle, _gridRows);
            var avg = unsolvedCells.Average(y => y.Value);
            var cellsToProbe = unsolvedCells.Where(x => x.Value >= avg).Take(16).Select(pair => pair.Key);

            var prevGuess = _isGuessing;
            _isGuessing = true;
            (Cell cell, int val) bestProbe= (cellsToProbe.First(),-1);

            foreach (var cell in cellsToProbe) {
                var currentGuessCount = _guessHistory.Count;
            
                PlaceCell(cell.Row, cell.Column, 1);
                var res = TrySolvingNoGuess();


                if (res == SolvingResult.Finished) {
                    //if (IsSolved()) {
                    //    var sol = GetCurrentSolution();
                    //    _solutions.Add(sol);
                    //    SolutionFound?.Invoke(this, sol);
                    //    if (_solutions.Count >= MaxSolutionsCount) {
                    //        return SolvingResult.Finished;
                    //    }
                    //}
                    if (_guessHistory.Count > bestProbe.val) {
                        bestProbe = (cell, _guessHistory.Count);
                    }

                    RestoreBeforeGuess(currentGuessCount);

                } else if (res == SolvingResult.Unsolvable) {
                    if (prevGuess) {
                        if (_guessHistory.Count > bestProbe.val) {
                            bestProbe = (cell, _guessHistory.Count);
                        }
                    } 

                    RestoreBeforeGuess(currentGuessCount);
                    _isGuessing = prevGuess;
                    PlaceCell(cell.Row, cell.Column,-1);
                    _isGuessing = true;
                }
                else if(res == SolvingResult.Cancelled) {
                    return res;
                }
            }
            var guessCount = _guessHistory.Count;
            PlaceCell(bestProbe.cell.Row, bestProbe.cell.Column, 1);
            var solveRes = Solve();

            if (solveRes == SolvingResult.Finished) {
                //if (IsSolved()) {
                //    var sol = GetCurrentSolution();
                //    _solutions.Add(sol);
                //    SolutionFound?.Invoke(this, sol);
                //    if (_solutions.Count >= MaxSolutionsCount) {
                //        return SolvingResult.Finished;
                //    }
                //}

                RestoreBeforeGuess(guessCount);

            }
            if (solveRes == SolvingResult.Cancelled) {
                return solveRes;
            }
            if (solveRes == SolvingResult.Unsolvable || _solutions.Count < MaxSolutionsCount) {
                RestoreBeforeGuess(guessCount);
                PlaceCell(bestProbe.cell.Row, bestProbe.cell.Column, -1);

                var solveRes2 = Solve();

                if (solveRes2 == SolvingResult.Finished) {
                    //if (IsSolved()) {
                    //    var sol = GetCurrentSolution();
                    //    _solutions.Add(sol);
                    //    SolutionFound?.Invoke(this, sol);
                    //    if (_solutions.Count >= MaxSolutionsCount) {
                    //        return SolvingResult.Finished;
                    //    }
                    //}
                }

                if (solveRes2 == SolvingResult.Finished && solveRes == SolvingResult.Unsolvable) {
                    return solveRes2;
                }

                return solveRes2;

            } 
            

            return solveRes;
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

                //Solve line
                var gridLine = lineToSolve.Orientation == LineOrientation.Row ? _gridRows[lineToSolve.Index] : _gridColumns[lineToSolve.Index];
                var cellsToUpdate = _lineSolver.SolveForNewCells(lineToSolve, gridLine);
                if (cellsToUpdate==null){
                    return SolvingResult.Unsolvable;
                }

                foreach (var cell in cellsToUpdate) {
                    if(lineToSolve.Orientation == LineOrientation.Row) {
                        PlaceCell(lineToSolve.Index, cell.Index, cell.Value);
                        _linesHeap.Remove(_puzzle.Rows[lineToSolve.Index]);
                    } else {
                        PlaceCell(  cell.Index, lineToSolve.Index, cell.Value);
                        _linesHeap.Remove(_puzzle.Columns[lineToSolve.Index]);
                    }
                }
            }

            return SolvingResult.Finished;
        }
        #endregion

        #region HelperMethods
        private void RestoreBeforeGuess(int guessesToLeaveCount) {
            if (guessesToLeaveCount < _guessHistory.Count) {
                var cellsToRestoreCount = _guessHistory.Count - guessesToLeaveCount;
                foreach (var cell in _guessHistory.GetRange(guessesToLeaveCount, cellsToRestoreCount)) {
                    _gridColumns[cell.Column][cell.Row] = 0;
                    _gridRows[cell.Row][cell.Column] = 0;
                }
                _guessHistory.RemoveRange(guessesToLeaveCount , cellsToRestoreCount);
            }
        }
        private void PlaceCell(int row, int col, int value) {
            PlaceCell(new Cell(row, col, value));
        }
        private void PlaceCell(Cell cell) {
            //Update both grids
            _gridColumns[cell.Column][cell.Row] = cell.Value;
            _gridRows[cell.Row][cell.Column] = cell.Value;
            //Increment lines priority
            _linesHeap.Increment(_puzzle.Rows[cell.Row]);
            _linesHeap.Increment(_puzzle.Columns[cell.Column]);

            if (_isGuessing) {
                _guessHistory.Add(cell);
            }
        }
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
