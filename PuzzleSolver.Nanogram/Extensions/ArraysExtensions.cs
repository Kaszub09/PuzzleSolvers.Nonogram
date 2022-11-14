using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace PuzzleSolvers.Nanogram {
    internal static class ArraysExtensions {
        internal static int[][] DeepCopy(this int[][] matrix) {
            var copy = new int[matrix.Length][];
            for (int i = 0; i < matrix.Length; i++) {
                copy[i] = matrix[i].ToArray();
            }
            return copy;
        }

        internal static string GetAsString(this int[][] matrix) {
           return string.Join("\n",
                matrix.Select(line => 
                    string.Join("",line.Select(x => x >0 ? "X" : (x < 0 ? "." : "0")))
                ));
        }
        internal static int[] DeepCopy(this int[] array) {
            return array.ToArray();
        }
    }
}
