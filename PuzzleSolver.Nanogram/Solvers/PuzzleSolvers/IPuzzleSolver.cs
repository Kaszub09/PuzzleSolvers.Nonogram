using System;
using System.Collections.Generic;
using System.Threading;

namespace PuzzleSolvers.Nanogram {
    public interface IPuzzleSolver {
        event EventHandler<ISolution> SolutionFound;
        /// <summary>
        /// Completes partially solved puzzle
        /// </summary>
        /// <param name="partialSolution"></param>
        /// <param name="solutions"></param>
        /// <param name="token"></param>
        /// <param name="limitNumberOfSolutions"></param>
        /// <returns></returns>

        SolvingResult CompleteForAllSolutions(ISolution partialSolution, out List<ISolution> solutions, CancellationToken token = default, int? limitNumberOfSolutions = null);
        /// <summary>
        /// Completes partially solved puzzle
        /// </summary>
        /// <param name="partialSolution"></param>
        /// <param name="solution"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        SolvingResult CompleteForAnySolutions(ISolution partialSolution, out ISolution solution, CancellationToken token = default);
        SolvingResult SolveForAnySolution(IPuzzle puzzle, out ISolution solution, CancellationToken token = default);
        SolvingResult SolveForManySolutions(IPuzzle puzzle, out List<ISolution> solutions, CancellationToken token = default, int? limitNumberOfSolutions = null);
        SolvingResult TrySolvingWithoutGuessing(IPuzzle puzzle, out ISolution partialSolution, CancellationToken token = default);
    }
}