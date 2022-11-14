using PuzzleSolvers.Nanogram;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PuzzleSolvers.Nanogram {
    internal class Line : ILine {
        public LineOrientation Orientation { get; }
        public int Index { get; }
        public int[] Clues { get; }
        public int Length { get; }
        public string CluesAsString { get; }
        public Line(LineOrientation orientation, int index,int[] clues, int length ) {
            Clues = clues.DeepCopy();
            Length = length;
            Index = index;
            Orientation = orientation;
            CluesAsString = string.Join(",", clues.Select(x => x.ToString()));
        }
     

        public int CompareTo(object obj) {
            if (obj is Line line) {
                if (Length > line.Length) {
                    return 1;
                } else if (Length < line.Length) {
                    return -1;
                } else {
                    if (Clues.Length > line.Clues.Length) {
                        return 1;
                    } else if (Clues.Length < line.Clues.Length) {
                        return -1;
                    } else {
                        for(int i =0;i < Clues.Length; i++) {
                            var clueComp = Clues[i].CompareTo(  line.Clues[i]);
                            if (clueComp != 0) {
                                return clueComp;
                            }
                        }
                    }
                }
                return 0;
            }
            throw new ArgumentException("Argument is of another type",nameof(obj));
        }
    }
}
