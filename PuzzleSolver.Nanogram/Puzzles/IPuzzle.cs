using System;
using System.Collections.Generic;

namespace PuzzleSolvers.Nanogram {
    /// <summary>
    /// Defines whole puzzle.
    /// </summary>
    public interface IPuzzle: IComparable {
        /// <summary>
        /// Clues for columns in LeftToRight order.
        /// </summary>
        ILine[] Columns { get; }
        /// <summary>
        /// Clues for rows in TopToBottom order.
        /// </summary>
        ILine[] Rows { get; }
        /// <summary>
        /// Vertical length of grid. Equals to number of rows.
        /// </summary>
        int ColumnLength { get; }
        /// <summary>
        /// Horizontal length of grid. Equals to number of columns.
        /// </summary>
        int RowLength { get; }     
    }
}