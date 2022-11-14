using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleSolvers.Nanogram {
    internal struct LineCell {
        public int Index { get; set; }
        public int Value { get; set; }

        public LineCell(int index, int value) {
            Index = index;
            Value = value;
        }

        //public override bool Equals(object obj) {
        //    return obj is LineCell cell &&
        //           Index == cell.Index &&
        //           Value == cell.Value;
        //}
    }
}
