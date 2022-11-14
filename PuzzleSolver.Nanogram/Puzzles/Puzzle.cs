using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleSolvers.Nanogram {
    internal class Puzzle : IPuzzle {
        public ILine[] Rows { get; }
        public ILine[] Columns { get; }
        public int RowLength { get; }
        public int ColumnLength { get; }

        public Puzzle(ILine[] rows, ILine[] columns) {
            Rows = rows;
            Columns = columns;
            RowLength = columns.Length;
            ColumnLength = rows.Length;
        }

        public int CompareTo(object obj) {
            if (obj is Puzzle puzzle) {
                if (RowLength > puzzle.RowLength) {
                    return 1;
                }else if (RowLength < puzzle.RowLength) {
                    return -1;
                } else {
                    if (ColumnLength > puzzle.ColumnLength) {
                        return 1;
                    } else if (ColumnLength < puzzle.ColumnLength) {
                        return -1;
                    } else {
                        foreach(var row in Rows) {
                            var rowComp = row.CompareTo(puzzle.Rows[row.Index]);
                            if (rowComp!=0) {
                                return rowComp;
                            }
                        }
                        foreach (var column in Columns) {
                            var rowCol = column.CompareTo(puzzle.Columns[column.Index]);
                            if (rowCol != 0) {
                                return rowCol;
                            }
                        }
                    }
                }
                
            }

            return 0;
        }
    }
}
