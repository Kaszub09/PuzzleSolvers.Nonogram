using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleSolvers.Nanogram {
    internal struct Cell {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Value { get; set; }

        public Cell(int row, int column, int value=0) {
            Row = row;
            Column = column;
            Value = value;
        }
    }
}
