
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PuzzleSolvers.Nanogram.Tests.FunctioningTests.Solvers {
    public class PuzzleSolversPartial {
        #region SingleSolvable
        [Theory]
        [MemberData(nameof(CompleteSingleSolutionNoGuessData))]
        internal void CompleteSingleSolutionNoGuess(int[][] rows, int[][] columns, int[][] partSolutionGrid, ISolution solution) {
            var partSolution = Factory.CreateSolution(Factory.CreatePuzzle(rows, columns), partSolutionGrid);
            var puzleSolver = Factory.CreatePuzzleSolver();

            Assert.Equal(SolvingResult.Finished, puzleSolver.CompleteForAllSolutions(partSolution, out List<ISolution> solverSolutions));
            Assert.Equal(solution, solverSolutions[0]);
            Assert.Single(solverSolutions);

            Assert.Equal(SolvingResult.Finished, puzleSolver.CompleteForAnySolutions(partSolution, out ISolution solverSolution));
            Assert.Equal(solution, solverSolution);
        }
        internal static IEnumerable<object[]> CompleteSingleSolutionNoGuessData() {
            //5x5
            yield return new object[] {
                new int[][]{
                    new int[] {2},
                    new int[] {1},
                    new int[] {3},
                    new int[] {3},
                    new int[] {1,3}
                },
                new int[][]{
                    new int[] {3},
                    new int[] {2},
                    new int[] {3},
                    new int[] {2,1},
                    new int[] {1,1}
                },
                new int[][]{
                    new int[] {-1,-1,-1, 1, 1},
                    new int[] {-1,-1,-1, 1,-1},
                    new int[] { 1, 0, 0,-1,-1},
                    new int[] { 1, 1, 1,-1,-1},
                    new int[] { 1,-1, 1, 1, 0}
                },
                Factory.CreateSolution(
                    null,
                    new int[][]{
                        new int[] {-1,-1,-1, 1, 1},
                        new int[] {-1,-1,-1, 1,-1},
                        new int[] { 1, 1, 1,-1,-1},
                        new int[] { 1, 1, 1,-1,-1},
                        new int[] { 1,-1, 1, 1, 1}
                    }
                )
            };
            //4x4 from multi
            yield return new object[] {
                new int[][]{
                    new int[] {1,1},
                    new int[] { 1, 1 },
                    new int[] {1,1},
                    new int[] { 1, 1 }
                },
                new int[][]{
                    new int[] {1,1},
                    new int[] { 1, 1 },
                    new int[] {1,1},
                    new int[] { 1, 1 }
                },
                new int[][]{
                    new int[] {-1, 0, 0, 0},
                    new int[] { 0, 0, 0, 0},
                    new int[] { 0, 0, 0, 0},
                    new int[] { 0, 0, 0, 0}
                },
                Factory.CreateSolution(null,
                    new int[][]{
                        new int[] {-1, 1,-1, 1},
                        new int[] { 1,-1, 1,-1},
                        new int[] {-1, 1,-1, 1},
                        new int[] { 1,-1, 1,-1}
                    }
                )
            };
            //2x2  solved
            yield return new object[] {
                new int[][]{
                    new int[] { 1 },
                    new int[] { 1 }
                },
                new int[][]{
                    new int[] { 1 },
                    new int[] { 1 }
                },
                new int[][]{
                    new int[] { 1,-1},
                    new int[] {-1, 1}
                },
                Factory.CreateSolution(
                    null,
                    new int[][]{
                        new int[] { 1,-1},
                        new int[] {-1, 1}
                    }
                )
            };
        }
        #endregion

        #region Unsolvable
        [Theory]
        [MemberData(nameof(CompleteUnsolvableData))]
        internal void CompleteUnsolvable(int[][] rows, int[][] columns, int[][] partSolutionGrid) {
            var partSolution = Factory.CreateSolution(Factory.CreatePuzzle(rows, columns), partSolutionGrid);
            var puzleSolver = Factory.CreatePuzzleSolver();

            Assert.Equal(SolvingResult.Unsolvable, puzleSolver.CompleteForAllSolutions(partSolution, out List<ISolution> solverSolutions));
            Assert.Null(solverSolutions);

            Assert.Equal(SolvingResult.Unsolvable, puzleSolver.CompleteForAnySolutions(partSolution, out ISolution solverSolution));
            Assert.Null(solverSolution);
        }
        internal static IEnumerable<object[]> CompleteUnsolvableData() {
            //3x2
            yield return new object[] {
                new int[][]{
                    new int[] {2},
                    new int[] {2},
                    new int[] {2}
                },
                new int[][]{
                    new int[] {1},
                    new int[] {1}
                },
                new int[][]{
                    new int[] { 1,-1},
                    new int[] { 1,-1},
                    new int[] { 1, 0}
                }
            };
            //3x2
            yield return new object[] {
                new int[][]{
                    new int[] {2},
                    new int[] {2},
                    new int[] {2}
                },
                new int[][]{
                    new int[] {1},
                    new int[] {1}
                },
                new int[][]{
                    new int[] { 1,-1},
                    new int[] { 1,-1},
                    new int[] { 1, 1}
                }
            };
        }
        #endregion

        #region MultiSolvable
        [Theory]
        [MemberData(nameof(CompleteMultiSolutionsData))]
        internal void CompleteMultiSolutions(int[][] rows, int[][] columns, int[][] partSolutionGrid, List<ISolution> solutions) {
            var partSolution = Factory.CreateSolution(Factory.CreatePuzzle(rows, columns), partSolutionGrid);
            var puzleSolver = Factory.CreatePuzzleSolver();

            Assert.Equal(SolvingResult.Finished, puzleSolver.CompleteForAllSolutions(partSolution, out List<ISolution> solverSolutions));
            solverSolutions.Sort();
            solutions.Sort();
            Assert.Equal(solutions, solverSolutions);

            Assert.Equal(SolvingResult.Finished, puzleSolver.CompleteForAnySolutions(partSolution, out ISolution solverSolution));
            Assert.Contains(solverSolution, solutions);

        }
        internal static IEnumerable<object[]> CompleteMultiSolutionsData() {
            //4x4 2 solutions
            yield return new object[] {
                new int[][]{
                    new int[] {1,1},
                    new int[] { 1, 1 },
                    new int[] {1,1},
                    new int[] { 1, 1 }
                },
                new int[][]{
                    new int[] {1,1},
                    new int[] { 1, 1 },
                    new int[] {1,1},
                    new int[] { 1, 1 }
                },
                new int[][]{
                        new int[] { 0, 0, 0, 0},
                        new int[] { 0, 0, 0, 0},
                        new int[] { 0, 0, 0, 0},
                        new int[] { 0, 0, 0, 0}
                },
                new List<ISolution>() {Factory.CreateSolution(null,
                    new int[][]{
                        new int[] { 1,-1, 1,-1},
                        new int[] {-1, 1,-1, 1},
                        new int[] { 1,-1, 1,-1},
                        new int[] {-1, 1,-1, 1}
                    }),
                    Factory.CreateSolution(null,
                    new int[][]{
                        new int[] {-1, 1,-1, 1},
                        new int[] { 1,-1, 1,-1},
                        new int[] {-1, 1,-1, 1},
                        new int[] { 1,-1, 1,-1}
                    })  }
                };
            //5x2 4 solutions
            yield return new object[] {
                new int[][]{
                    new int[] {1},
                    new int[] {1},
                    new int[] {0},
                    new int[] {1},
                    new int[] {1}
                },
                new int[][]{
                    new int[] {1,1},
                    new int[] {1,1}
                },
                new int[][]{
                        new int[] { 1, 0},
                        new int[] { 0, 0},
                        new int[] {-1,-1},
                        new int[] { 0, 0},
                        new int[] { 0, 0}
                },
                new List<ISolution>() {Factory.CreateSolution(null,
                    new int[][]{
                        new int[] { 1,-1},
                        new int[] {-1, 1},
                        new int[] {-1,-1},
                        new int[] { 1,-1},
                        new int[] {-1, 1}
                    }),
                    Factory.CreateSolution(null,
                    new int[][]{
                        new int[] { 1,-1},
                        new int[] {-1, 1},
                        new int[] {-1,-1},
                        new int[] {-1, 1},
                        new int[] { 1,-1}
                    }) }
                };
        }
        #endregion

    }
}
