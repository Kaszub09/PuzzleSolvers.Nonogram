
using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleSolvers.Nanogram {
    internal class LineException : Exception {
        public ILine Line { get; }
        internal LineException(ILine line) {
            Line = line;
        }
        internal LineException(ILine line,string message):base(message) {
            Line = line;
        }
    }
}
