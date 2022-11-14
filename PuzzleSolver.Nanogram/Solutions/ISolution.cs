using System;
using System.Collections;

namespace PuzzleSolvers.Nanogram {
    /// <summary>
    /// Describes solution for a puzzle. 
    /// Cell values: -1 = empty; 0 = not solved; 1 = filled
    /// </summary>
    public interface ISolution : IComparable {
        /// <summary>
        /// First index denotes row, second index  denotes column.
        /// </summary>
        int[][] Grid { get; }
        /// <summary>
        /// Solution is complete if all cells are solved.
        /// </summary>
        bool IsComplete { get; }
        IPuzzle ParentPuzzle { get; }

        int[] GetGridColumn(int index);
        int[] GetGridRow(int index);
        int GetCellValue(int row, int column);
    }
}