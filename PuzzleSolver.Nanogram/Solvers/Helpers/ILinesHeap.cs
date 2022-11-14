
using System.Collections.Generic;

namespace PuzzleSolvers.Nanogram {
    internal interface ILinesHeap {
        void Add(ILine line);
        ILine GetKeyWithMaxValue();
        void Increment(ILine line);
        bool IsEmpty();
        void Populate(IEnumerable<ILine> lines);
        void Populate(IPuzzle puzzle);
        void Remove(ILine line);
    }
}