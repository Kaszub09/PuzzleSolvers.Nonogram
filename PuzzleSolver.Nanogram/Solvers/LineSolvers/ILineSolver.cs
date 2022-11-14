using System.Collections.Generic;

namespace PuzzleSolvers.Nanogram {
    internal interface ILineSolver {
        bool KeepTrackOfSolutions { get; set; }
        /// <summary>
        /// null means it's not solvable
        /// </summary>
        /// <param name="line"></param>
        /// <param name="gridLine"></param>
        /// <returns></returns>
        List<LineCell> SolveForNewCells(ILine line, int[] gridLine);
         void ClearCache();
    }
}