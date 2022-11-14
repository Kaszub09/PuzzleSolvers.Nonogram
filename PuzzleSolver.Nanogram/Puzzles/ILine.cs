using PuzzleSolvers.Nanogram;
using System;
using System.Collections.Generic;

namespace PuzzleSolvers.Nanogram {
    /// <summary>
    /// Describes single line with clues in puzzle, as well as it's position on grid.
    /// </summary>
    public interface ILine : IComparable {
        /// <summary>
        /// Line orientation on grid.
        /// </summary>
        LineOrientation Orientation { get; }
        /// <summary>
        /// Line index on grid. LeftToRight for columns,  TopToBottom for rows.
        /// </summary>
        int Index { get; }
        /// <summary>
        /// LeftToRight for rows, TopToBottom for columns.
        /// </summary>
        int[] Clues { get; }
        /// <summary>
        /// Length of line on grid
        /// </summary>
        int Length { get; }

        string CluesAsString { get; }

    }
}