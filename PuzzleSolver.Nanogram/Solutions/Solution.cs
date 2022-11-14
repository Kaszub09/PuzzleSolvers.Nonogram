
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics.SymbolStore;
using PuzzleSolvers.Nanogram;

namespace PuzzleSolvers.Nanogram {
    internal class Solution : ISolution {
        public IPuzzle ParentPuzzle { get; }
        public int[][] Grid { get; }
        public bool IsComplete { get; }

        public Solution(IPuzzle parentPuzzle, int[][] gridRowCol) {
            ParentPuzzle = parentPuzzle;
            Grid = gridRowCol.DeepCopy();
            IsComplete = Grid.Where(row => row.Contains(0)).Count() == 0;
        }

        public int[] GetGridRow(int index) {
            return Grid[index];
        }

        public int[] GetGridColumn(int index) {
            return Grid.Select((row) => row[index]).ToArray();
        }

        public int GetCellValue(int row, int column) {
            return Grid[row][column];
        }

        public override bool Equals(object obj) {
            if (obj is Solution solution) {
                if (Grid.Length != solution.Grid.Length) {
                    return false;
                }

                for (int i = 0; i < Grid.Length; i++) {
                    if (Grid[i].Length != solution.Grid[i].Length) {
                        return false;
                    }
                    for (int j = 0; j < Grid[i].Length; j++) {
                        if (!Grid[i][j].Equals(solution.Grid[i][j])) {
                            return false;
                        }
                    }
                }

                return true;
            }
            return false;
        }

        public int CompareTo(object obj) {
            if (obj is Solution s2) {
                for (int i = 0; i < Grid.Length; i++) {
                    for (int j = 0; j < Grid[i].Length; j++) {
                        if (Grid[i][j] > s2.Grid[i][j]) {
                            return 1;
                        } else if (Grid[i][j] < s2.Grid[i][j]) {
                            return -1;
                        }
                    }
                }
            }

            return 0;
        }


    }
}
