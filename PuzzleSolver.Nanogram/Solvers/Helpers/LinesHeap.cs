
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuzzleSolvers.Nanogram {
    internal class LinesHeap : ILinesHeap {
        private Dictionary<ILine, int> _heap = new Dictionary<ILine, int>();

        public void Add(ILine line) {
            _heap.Add(line, 0);
        }
        public void Remove(ILine line) {
            if (_heap.ContainsKey(line)) {
                _heap.Remove(line);
            }
        }
        public void Increment(ILine line) {
            if (_heap.ContainsKey(line)) {
                _heap[line]++;
            } else {
                _heap.Add(line, 0);
            }
        }

        public ILine GetKeyWithMaxValue() {
            //TODO or you can keep track of maximum value pair, and update after each increment. To check if it's faster
            var maxPair = _heap.First();
            foreach (var pair in _heap) {
                if (pair.Value > maxPair.Value) {
                    maxPair = pair;
                }
            }
            return maxPair.Key;
        }

        public bool IsEmpty() {
            return _heap.Count == 0;
        }

        public void Populate(IPuzzle puzzle) {
            for (int i = 0; i < puzzle.Rows.Length; i++) {
                if (!_heap.ContainsKey(puzzle.Rows[i])) {
                    _heap.Add(puzzle.Rows[i], 0);
                }
            }
            for (int i = 0; i < puzzle.Columns.Length; i++) {
                if (!_heap.ContainsKey(puzzle.Columns[i])) {
                    _heap.Add(puzzle.Columns[i], 0);
                }
            }
        }

        public void Populate(IEnumerable<ILine> lines) {
            foreach (var line in lines) {
                Add(line);
            }
        }
    }
}
