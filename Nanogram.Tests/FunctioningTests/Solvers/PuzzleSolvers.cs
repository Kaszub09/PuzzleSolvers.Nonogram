
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PuzzleSolvers.Nanogram.Tests.FunctioningTests.Solvers {
    public class PuzzleSolvers {
        #region NoGuessSolving

        [Theory]
        [MemberData(nameof(TrySolvingNoGuessData))]
        internal void TrySolvingNoGuess(int[][] rows, int[][] columns, ISolution solution) {
            var puzzle = Factory.CreatePuzzle(rows, columns);
            var puzleSolver = Factory.CreatePuzzleSolver();

            Assert.Equal(SolvingResult.Finished, puzleSolver.TrySolvingWithoutGuessing(puzzle, out ISolution solverSolutionNoGuess));
            Assert.Equal(solution, solverSolutionNoGuess);
        }
        internal static IEnumerable<object[]> TrySolvingNoGuessData() {
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
                Factory.CreateSolution(null,
                new int[][]{
                    new int[] { 0, 0, 0, 0},
                    new int[] { 0, 0, 0, 0},
                    new int[] { 0, 0, 0, 0},
                    new int[] { 0, 0, 0, 0}
                })  
                };
        }

            #endregion

        #region SingleSolvable
            [Theory]
        [MemberData(nameof(SolveSingleSolutionData))]
        internal void SolveSingleSolution(int[][] rows, int[][] columns, ISolution solution) {
            var puzzle = Factory.CreatePuzzle(rows, columns);
            var puzleSolver = Factory.CreatePuzzleSolver();

            Assert.Equal(SolvingResult.Finished, puzleSolver.SolveForManySolutions(puzzle, out List<ISolution> solverSolutions));
            Assert.Equal(solution, solverSolutions[0]);
            Assert.Single(solverSolutions);
            
            Assert.Equal(SolvingResult.Finished, puzleSolver.SolveForAnySolution(puzzle, out ISolution solverSolution));
            Assert.Equal(solution, solverSolution);
        }
        internal static IEnumerable<object[]> SolveSingleSolutionData() {
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
            //2x5
            yield return new object[] {
                new int[][]{
                    new int[] {3},
                    new int[] {4}
                },
                new int[][]{
                    new int[] {1},
                    new int[] {2},
                    new int[] {2},
                    new int[] {1},
                    new int[] {1}
                },
                Factory.CreateSolution(
                    null,
                    new int[][]{
                        new int[] { 1, 1, 1,-1,-1},
                        new int[] {-1, 1, 1, 1, 1}
                    }
                )
            };
            //40x40
            yield return new object[] {
                new int[][]{
                    new int[]{17,32},
                    new int[]{16,32},
                    new int[]{15,1,22},
                    new int[]{14,18},
                    new int[]{13,15},
                    new int[]{12,13},
                    new int[]{11,8,12},
                    new int[]{9,13,11},
                    new int[]{8,15,9},
                    new int[]{7,16,2,5},
                    new int[]{6,17,6},
                    new int[]{5,18,7},
                    new int[]{5,18,6},
                    new int[]{4,17,6},
                    new int[]{4,1,16,5},
                    new int[]{3,2,12,2,5},
                    new int[]{3,1,9,4,4},
                    new int[]{2,2,5,5,4},
                    new int[]{2,2,3,7,3},
                    new int[]{2,3,8,3},
                    new int[]{1,5,9,3},
                    new int[]{1,6,10,3},
                    new int[]{1,7,12,3},
                    new int[]{1,8,12,2},
                    new int[]{1,8,13,2},
                    new int[]{1,8,14,2},
                    new int[]{1,8,13,2},
                    new int[]{1,5,1,11,1,3},
                    new int[]{1,5,11,3},
                    new int[]{1,5,11,3},
                    new int[]{2,5,9,2,3},
                    new int[]{2,3,2,5,6,3},
                    new int[]{2,3,9,2,6,4},
                    new int[]{3,4,9,9,4},
                    new int[]{3,3,9,9,4},
                    new int[]{3,3,9,8,5},
                    new int[]{4,4,9,6,5},
                    new int[]{4,5,7,5,6},
                    new int[]{5,5,4,2,4,7},
                    new int[]{6,5,2,4,2,7},
                    new int[]{7,5,6,4,8},
                    new int[]{8,3,6,6,9},
                    new int[]{9,3,5,7,10},
                    new int[]{10,8,4,11},
                    new int[]{11,3,11},
                    new int[]{9,3,2,10},
                    new int[]{5,7,16},
                    new int[]{4,10,19},
                    new int[]{3,44},
                    new int[]{2,47}
                },
                new int[][]{
                    new int[]{50},
                    new int[]{20,20},
                    new int[]{17,16},
                    new int[]{15,12,1},
                    new int[]{13,9,1},
                    new int[]{11,11,7,1},
                    new int[]{10,17,6,2},
                    new int[]{9,20,5,2},
                    new int[]{8,2,18,8},
                    new int[]{7,11,5,2,4},
                    new int[]{7,6,4,6},
                    new int[]{6,6,4,5},
                    new int[]{5,4,4,5},
                    new int[]{4,5,4},
                    new int[]{3,4,4},
                    new int[]{2,3,3,3},
                    new int[]{1,6,1,1,3},
                    new int[]{6,2,3},
                    new int[]{2,2,8,2,2},
                    new int[]{3,8,10,1,2},
                    new int[]{2,11,7,4,2},
                    new int[]{2,11,13,2},
                    new int[]{2,13,6,6,2},
                    new int[]{2,13,13,2},
                    new int[]{2,13,3,6,2},
                    new int[]{2,12,2},
                    new int[]{2,12,2,2},
                    new int[]{2,11,2,3,2},
                    new int[]{3,10,5,4,2},
                    new int[]{3,9,7,4,2},
                    new int[]{3,9,9,3,2},
                    new int[]{3,8,11,3,3},
                    new int[]{4,7,11,3,2,3},
                    new int[]{4,6,12,4,3},
                    new int[]{4,6,12,6,4},
                    new int[]{5,4,13,8,4},
                    new int[]{5,2,23,5},
                    new int[]{6,14,8,5},
                    new int[]{7,13,7,4},
                    new int[]{8,12,5,2,4},
                    new int[]{8,11,5,8},
                    new int[]{10,8,2,9},
                    new int[]{10,4,1,10},
                    new int[]{9,1,12},
                    new int[]{9,4,13},
                    new int[]{16,15},
                    new int[]{18,18},
                    new int[]{23,23},
                    new int[]{50},
                    new int[]{50}
                },
                Factory.CreateSolution(
                    null,
                    new int[][]{
                        new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1, 1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1, 1, 1,-1,-1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1,-1,-1,-1,-1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1,-1,-1,-1,-1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1, 1, 1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1,-1,-1,-1,-1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1, 1, 1},
                        new int[]{1, 1,-1,-1,-1,-1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1, 1, 1},
                        new int[]{1, 1,-1,-1,-1,-1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1, 1},
                        new int[]{1, 1,-1,-1,-1,-1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1, 1},
                        new int[]{1,-1,-1,-1,-1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1, 1},
                        new int[]{1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1, 1},
                        new int[]{1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1, 1, 1, 1},
                        new int[]{1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1},
                        new int[]{1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1},
                        new int[]{1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1},
                        new int[]{1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1, 1, 1},
                        new int[]{1,-1,-1,-1,-1, 1, 1, 1, 1, 1,-1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1, 1,-1,-1,-1,-1, 1, 1, 1},
                        new int[]{1,-1,-1,-1,-1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1},
                        new int[]{1,-1,-1,-1,-1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1},
                        new int[]{1, 1,-1,-1,-1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1, 1, 1,-1,-1,-1,-1,-1, 1, 1, 1},
                        new int[]{1, 1,-1,-1,-1,-1, 1, 1, 1,-1,-1,-1,-1,-1,-1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1,-1,-1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1, 1},
                        new int[]{1, 1,-1,-1,-1,-1, 1, 1, 1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1, 1, 1,-1,-1,-1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1, 1, 1},
                        new int[]{1, 1, 1,-1,-1,-1, 1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1, 1, 1},
                        new int[]{1, 1, 1,-1,-1,-1,-1, 1, 1, 1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1, 1, 1},
                        new int[]{1, 1, 1,-1,-1,-1,-1,-1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1,-1,-1,-1,-1, 1, 1, 1, 1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1,-1,-1,-1, 1, 1, 1, 1,-1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1,-1,-1, 1, 1,-1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1,-1,-1, 1, 1, 1, 1, 1, 1,-1,-1,-1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1, 1,-1,-1,-1, 1, 1, 1, 1, 1, 1,-1,-1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1,-1, 1, 1, 1, 1, 1,-1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1,-1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1,-1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1, 1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1, 1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1, 1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        new int[]{1, 1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
                    })
            };
        }
        #endregion

        #region Unsolvable
        [Theory]
        [MemberData(nameof(DetectUnsolvableData))]
        internal void DetectUnsolvable(int[][] rows, int[][] columns) {
            var puzzle = Factory.CreatePuzzle(rows, columns);
            var puzleSolver = Factory.CreatePuzzleSolver();

            Assert.Equal(SolvingResult.Unsolvable, puzleSolver.SolveForManySolutions(puzzle, out List<ISolution> solverSolutions));
            Assert.Null(solverSolutions);

            Assert.Equal(SolvingResult.Unsolvable, puzleSolver.SolveForAnySolution(puzzle, out ISolution solverSolution));
            Assert.Null(solverSolution);
        }
        internal static IEnumerable<object[]> DetectUnsolvableData() {
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
                }
            };
        }
        #endregion

        #region MultiSolvable
        [Theory]
        [MemberData(nameof(SolveMultiSolutionsData))]
        internal void SolveMultiSolutions(int[][] rows, int[][] columns, List<ISolution> solutions) {
            var puzzle = Factory.CreatePuzzle(rows, columns);
            var puzleSolver = Factory.CreatePuzzleSolver();

            Assert.Equal(SolvingResult.Finished, puzleSolver.SolveForManySolutions(puzzle, out List<ISolution> solverSolutions));
            solverSolutions.Sort();
            solutions.Sort();
            Assert.Equal(solutions,solverSolutions);

            Assert.Equal(SolvingResult.Finished, puzleSolver.SolveForAnySolution(puzzle, out ISolution solverSolution));
            Assert.Contains(solverSolution, solutions);
        }
        internal static IEnumerable<object[]> SolveMultiSolutionsData() {
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
                    }),
                    Factory.CreateSolution(null,
                    new int[][]{
                        new int[] {-1, 1},
                        new int[] { 1,-1},
                        new int[] {-1,-1},
                        new int[] {-1, 1},
                        new int[] { 1,-1}
                    }),
                    Factory.CreateSolution(null,
                    new int[][]{
                        new int[] {-1, 1},
                        new int[] { 1,-1},
                        new int[] {-1,-1},
                        new int[] { 1,-1},
                        new int[] {-1, 1}
                    })  }
                };
        }
        #endregion

    }
}
