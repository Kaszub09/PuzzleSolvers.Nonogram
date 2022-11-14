using PuzzleSolvers.Nanogram;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PuzzleSolvers.Nanogram {
    internal class LeftRightLineSolver : ILineSolver {
        private Dictionary<string, List<LineCell>> _solveHistory = new Dictionary<string, List<LineCell>>( );

        public bool KeepTrackOfSolutions { get; set; } = false;

        public List<LineCell> SolveForNewCells(ILine line, int[] gridLine) {
            if (KeepTrackOfSolutions) {
                //TODO check if calcualtion part of hash when line is created speeds up solving
                var lineHash = GetGroupsLineHash(line, gridLine);
                if (_solveHistory.ContainsKey(lineHash)) {
                    //Already solved
                    return _solveHistory[lineHash];
                } else {
                    //Not solved before, solve and remember solution
                    var newCells = GetNewCells(GetLeftLine(line, gridLine), GetRightLine(line, gridLine), gridLine);
                    _solveHistory.Add(lineHash, newCells);
                    return newCells;
                }
            } else {
                return GetNewCells(GetLeftLine(line, gridLine), GetRightLine(line, gridLine), gridLine);
            }   
        }

        public void ClearCache() {
            _solveHistory.Clear();
        }
            

        /// <summary>
        /// Gets hash based on line groups and gridline (length, index, orientation of line doesn't matter for solving single line)
        /// </summary>
        /// <param name="line"></param>
        /// <param name="gridLine"></param>
        /// <returns></returns>
        private string GetGroupsLineHash(ILine line, int[] gridLine) {
            var sb = new StringBuilder();
            foreach (var group in line.Clues) {
                sb.Append(group);
                sb.Append('_');
            }
            foreach (var cell in gridLine) {
                sb.Append(cell);
                sb.Append('_');
            }
            return sb.ToString();
        }

        /// <summary>
        /// Get leftmost (based on groups) available line, which doesn't contradict already solved cells
        /// </summary>
        /// <param name="line"></param>
        /// <param name="gridLine"></param>
        /// <returns></returns>
        private int[] GetLeftLine(ILine line, int[] gridLine) {
            return GetCompleteLine(line, GetGroupsStartIdxs(line, gridLine));
        }

        private List<int> GetGroupsStartIdxs(ILine line, int[] gridLine) {
            var gpStartIdxs = new List<int>(line.Clues.Length);
            int pos = 0, groupIdx = 0, reqSpace = line.Clues.Length == 0 ? 0 : line.Clues.Sum() + line.Clues.Length - 1;

            while (pos < line.Length) {
                if (groupIdx < line.Clues.Length && reqSpace <= gridLine.Length - pos && CanPlaceGroup(pos, line.Clues[groupIdx], gridLine)) {
                    
                    //Remember when to place group
                    gpStartIdxs.Add(pos);
                    //move by group size + one empty cell
                    pos += line.Clues[groupIdx] + 1;
                    //update required remaining space
                    reqSpace -= line.Clues[groupIdx] + 1;
                    //next group
                    groupIdx++;

                } else {
                    //Must be empty cell
                    if (gridLine[pos] <= 0) {
                        pos++;
                    } else {

                        //Can't be empty, keep removing previous groups till found that can
                        while (gpStartIdxs.Count > 0 && gridLine[pos] > 0) {
                            pos = gpStartIdxs[gpStartIdxs.Count - 1];
                            reqSpace += line.Clues[gpStartIdxs.Count - 1] + 1;
                            gpStartIdxs.RemoveAt(gpStartIdxs.Count - 1);

                            groupIdx--;
                        }
                        //If removed all groups and cell can't be empty, it's contradiction
                        if (gridLine[pos] > 0) {
                            throw new LineException(line);
                        }
                        pos++;
                    }
                }
            }
            if (groupIdx < line.Clues.Length) {
                throw new LineException(line);
            }

            return gpStartIdxs;
        }

        private bool CanPlaceGroup(int pos, int groupSize, int[] gridLine) {
            var afterGroup = pos + groupSize;
            //Check if there is place for whole group
            if (afterGroup > gridLine.Length) {
                return false;
            }
            //Can't have empty cell
            for (int i = pos; i < afterGroup; i++) {
                if (gridLine[i] < 0) {
                    return false;
                }
            }
            //Must have empty cell after group
            if (afterGroup < gridLine.Length && gridLine[afterGroup] > 0) {
                return false;
            }
            //Not needed, all solving is leftToRight
            ////Must have empty cell before group
            //var beforeGroup = pos - 1;
            //if (beforeGroup > 0 && gridLine[beforeGroup] > 0) {
            //    return false;
            //}
            return true;
        }

        private int[] GetCompleteLine(ILine line, List<int> gpStartIdxs) {
            var completeLine = new int[line.Length];
            int emptyNumber = -1, cellIdx = 0;

            //Empty cells before any filled cell
            for (int i = 0; i < (gpStartIdxs.Count > 0 ? gpStartIdxs[0] : line.Length); i++) {
                completeLine[i] = emptyNumber;
            }

            for (int groupIdx = 0; groupIdx < gpStartIdxs.Count; groupIdx++) {
                //Filled cells
                cellIdx = gpStartIdxs[groupIdx];
                for (int i = cellIdx; i < cellIdx + line.Clues[groupIdx]; i++) {
                    completeLine[i] = groupIdx + 1;
                }

                //Empty cells
                emptyNumber--;
                cellIdx = cellIdx + line.Clues[groupIdx];
                int endIdx = groupIdx < gpStartIdxs.Count - 1 ? gpStartIdxs[groupIdx + 1] - 1 : line.Length - 1;
                for (int i = cellIdx; i <= endIdx; i++) {
                    completeLine[i] = emptyNumber;
                }
            }

            return completeLine;
        }

        /// <summary>
        /// Get rightmost (based on groups) available line, which doens't contradict already solved cells
        /// </summary>
        /// <param name="line"></param>
        /// <param name="gridLine"></param>
        /// <returns></returns>
        private int[] GetRightLine(ILine line, int[] gridLine) {
            //Reverse line and gridline
            var revLine = Factory.CreateLine(line.Orientation, line.Index, line.Clues.Reverse().ToArray(), line.Length);
            var revGridLine = gridLine.Reverse().ToArray();

            //Calculate groups for reversed line, then revese them
            var revGpStartIdxs = GetGroupsStartIdxs(revLine, revGridLine);
            var gpStartIdxs = new List<int>();
            for (int i = revGpStartIdxs.Count - 1; i >= 0; i--) {
                gpStartIdxs.Add(line.Length - 1 - (revGpStartIdxs[i] + revLine.Clues[i] - 1));
            }

            return GetCompleteLine(line, gpStartIdxs);
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


    }
}
