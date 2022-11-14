using PuzzleSolvers.Nanogram;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PuzzleSolvers.Nanogram {
    internal class LeftRightLineSolver2  {
        private int _maxHistoryCount = 50000;
        private Dictionary<string, List<LineCell>> _solveHistory = new Dictionary<string, List<LineCell>>(10000);

        public bool KeepTrackOfSolutions { get; set; } = false;

        public List<LineCell> SolveForNewCells(ILine line, int[] gridLine) {
            if (KeepTrackOfSolutions) {
                var lineHash = GetAsUniqueString(line, gridLine);
                if (_solveHistory.ContainsKey(lineHash)) {
                    //Already solved
                    return _solveHistory[lineHash];

                } else {
                    ////Limit dictionary size in order to limit memory usage
                    //if (_solveHistory.Count > _maxHistoryCount) {
                    //    _solveHistory.Clear();
                    //    _solveHistory = new Dictionary<string, List<LineCell>>(_maxHistoryCount);
                    //}

                    //Not solved before, solve and remember solution
                    var newCells = GetNewCells(line, gridLine);
                    _solveHistory.Add(lineHash, newCells);

                    return newCells;
                }
            } else {
                return GetNewCells(line, gridLine);
            }   
        }

        public void ClearCache() {
            _solveHistory.Clear();
        }

        private List<LineCell> GetNewCells(ILine line, int[] gridLine) {
            var leftLine = GetLeftLine(line, gridLine);
            if(leftLine == null) {
                return null;
            } else {
                return GetNewCells(leftLine, GetRightLine(line, gridLine), gridLine);
            }
        }

        private List<LineCell> GetNewCells(int[] leftLine, int[] rightLine, int[] gridLine) {
            var newCells = new List<LineCell>();
            for (int i = 0; i < leftLine.Length; i++) {
                if (gridLine[i] == 0 && leftLine[i] != 0 && leftLine[i] == rightLine[i]) {
                    if (leftLine[i] > 0) {
                        newCells.Add(new LineCell(i, 1));
                    } else {
                        newCells.Add(new LineCell(i, -1));
                    }
                }
            }
            return newCells;
        }


        private int[] GetLeftLine(ILine line, int[] gridLine) {
            var cluesStartIdxs = GetCluesStartIdxs(line, gridLine);
            if (cluesStartIdxs == null) {
                return null;
            } else {
                return GetCompleteLine(line, cluesStartIdxs);
            }     
        }
        private int[] GetRightLine(ILine line, int[] gridLine) {
            //Reverse line and gridline
            var revLine = Factory.CreateLine(line.Orientation, line.Index, line.Clues.Reverse().ToArray(), line.Length);
            var revGridLine = gridLine.Reverse().ToArray();

            //Calculate clues for reversed line, then revese them
            var revCluesStartIdxs = GetCluesStartIdxs(revLine, revGridLine);
            var cluesStartIdxs = new List<int>();
            for (int i = revCluesStartIdxs.Count - 1; i >= 0; i--) {
                cluesStartIdxs.Add(line.Length - 1 - (revCluesStartIdxs[i] + revLine.Clues[i] - 1));
            }

            return GetCompleteLine(line, cluesStartIdxs);
        }

        private List<int> GetCluesStartIdxs(ILine line, int[] gridLine) {
            var gpStartIdxs = new List<int>(line.Clues.Length);
            int pos = 0, clueIdx = 0, reqSpace = line.Clues.Length == 0 ? 0 : line.Clues.Sum() + line.Clues.Length - 1;

            while (pos < line.Length) {
                if (clueIdx < line.Clues.Length && reqSpace <= gridLine.Length - pos && CanPlace(pos, line.Clues[clueIdx], gridLine)) {

                    //Remember when to place clue
                    gpStartIdxs.Add(pos);
                    //move by clue value + one empty cell
                    pos += line.Clues[clueIdx] + 1;
                    //update required remaining space
                    reqSpace -= line.Clues[clueIdx] + 1;
                    //next clue
                    clueIdx++;

                } else {
                    //Must be empty cell
                    if (gridLine[pos] <= 0) {
                        pos++;
                    } else {

                        //Can't be empty, keep removing previous clues till found that can
                        while (gpStartIdxs.Count > 0 && gridLine[pos] > 0) {
                            pos = gpStartIdxs[gpStartIdxs.Count - 1];
                            reqSpace += line.Clues[gpStartIdxs.Count - 1] + 1;
                            gpStartIdxs.RemoveAt(gpStartIdxs.Count - 1);

                            clueIdx--;
                        }
                        //If removed all clues and cell can't be empty, it's contradiction
                        if (gridLine[pos] > 0) {
                            return null;
                        }
                        pos++;
                    }
                }
            }
            if (clueIdx < line.Clues.Length) {
                return null;
            }

            return gpStartIdxs;
        }

        private int[] GetCompleteLine(ILine line, List<int> cluesStartIdxs) {

            var completeLine = new int[line.Length];
            int emptyNumber = -1, cellIdx;

            //Empty cells before any filled cell
            for (int i = 0; i < (cluesStartIdxs.Count > 0 ? cluesStartIdxs[0] : line.Length); i++) {
                completeLine[i] = emptyNumber;
            }

            for (int clueIdx = 0; clueIdx < cluesStartIdxs.Count; clueIdx++) {
                //Filled cells
                cellIdx = cluesStartIdxs[clueIdx];
                for (int i = cellIdx; i < cellIdx + line.Clues[clueIdx]; i++) {
                    completeLine[i] = clueIdx + 1;
                }

                //Empty cells
                emptyNumber--;
                cellIdx = cellIdx + line.Clues[clueIdx];
                int endIdx = clueIdx < cluesStartIdxs.Count - 1 ? cluesStartIdxs[clueIdx + 1] - 1 : line.Length - 1;
                for (int i = cellIdx; i <= endIdx; i++) {
                    completeLine[i] = emptyNumber;
                }
            }

            return completeLine;
        }



        private bool CanPlace(int pos, int clueVal, int[] gridLine) {
            var afterClue = pos + clueVal;
            //Check if there is place for whole clue
            if (afterClue > gridLine.Length) {
                return false;
            }
            //Can't have empty cell
            for (int i = pos; i < afterClue; i++) {
                if (gridLine[i] < 0) {
                    return false;
                }
            }
            //Must have empty cell after clue
            if (afterClue < gridLine.Length && gridLine[afterClue] > 0) {
                return false;
            }
            //Not needed, all solving is leftToRight
            ////Must have empty cell before clue
            //var beforeClue = pos - 1;
            //if (beforeClue > 0 && gridLine[beforeClue] > 0) {
            //    return false;
            //}
            return true;
        }
        private string GetAsUniqueString(ILine line, int[] gridLine) {
            var sb = new StringBuilder(line.CluesAsString);
            foreach (var cell in gridLine) {
                sb.Append(cell);
            }
            return sb.ToString();
        }



    }
}
