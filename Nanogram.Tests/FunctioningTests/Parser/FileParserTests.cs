using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PuzzleSolvers.Nanogram.Tests.FunctioningTests.Parser {
    public class FileParserTests {
        #region StandardFiles
        [Theory]
        [MemberData(nameof(ParseStandardFilesData))]

        internal void ParseStandardFiles(int[][] rows, int[][] columns, string fileName) {
            var filePath = @"FunctioningTests\Parser\Files\Standard\" + fileName;
            Assert.True(File.Exists(filePath));

            var puzzle = Factory.CreatePuzzle(rows, columns);
            var parser = Factory.CreateFileParser();

            var parsedPuzzle = parser.ParseStandardFile(filePath);

            Assert.Equal(puzzle, parsedPuzzle);
        }

        internal static IEnumerable<object[]> ParseStandardFilesData() {
            yield return new object[] {
                new int[][]{
                    new int[] {1}
                },
                new int[][]{
                    new int[] {1 }
                },
                "1x1.txt"
            };
            yield return new object[] {
                new int[][]{
                    new int[] {1},
                    new int[] {0}
                },
                new int[][]{
                    new int[] {0},
                    new int[] {1},
                    new int[] {0}
                },
                "2x3.txt"
            };
            yield return new object[] {
                new int[][]{
                     new int[]{6},
                     new int[]{2,6},
                     new int[]{1,5},
                     new int[]{2},
                     new int[]{1},
                     new int[]{4,1,5},
                     new int[]{11,5},
                     new int[]{3,7,6},
                     new int[]{3,7,4,2},
                     new int[]{1,11,1,1},
                     new int[]{1,11,2},
                     new int[]{1,13,1},
                     new int[]{15,1},
                     new int[]{15,1},
                     new int[]{15,1},
                     new int[]{16,1,1},
                     new int[]{18,2},
                     new int[]{20},
                     new int[]{20},
                     new int[]{19},
                     new int[]{17},
                     new int[]{15},
                     new int[]{13},
                     new int[]{11},
                    new int[]{4,4}
                },
                new int[][]{
                     new int[]{9},
                     new int[]{2,8},
                     new int[]{3,10},
                     new int[]{2,12},
                     new int[]{2,14},
                     new int[]{19},
                     new int[]{20},
                     new int[]{20},
                     new int[]{1,19},
                     new int[]{3,19},
                     new int[]{21},
                     new int[]{1,19},
                     new int[]{2,2,16},
                     new int[]{3,1,17},
                     new int[]{3,5,14},
                     new int[]{3,4,9},
                     new int[]{2,4,7},
                     new int[]{2,2,1,7},
                     new int[]{1,2,4},
                     new int[]{2,4,4},
                     new int[]{3,5}
                },
                "webpbn034024"
            };
        }
        #endregion

        #region XMLfiles
        [Theory]
        [MemberData(nameof(ParseXMLfilesWithSolutionData))]
        internal void ParseXMLfilesWithSolution(int[][] rows, int[][] columns, int[][] solutionGrid, string fileName) {
            var filePath = @"FunctioningTests\Parser\Files\XML\" + fileName;
            Assert.True(File.Exists(filePath));

            var puzzle = Factory.CreatePuzzle(rows, columns);
            var solution = Factory.CreateSolution(puzzle, solutionGrid);

            var parser = Factory.CreateFileParser();
            parser.ParseXMLFile(filePath, out IPuzzle parsedPuzzle, out List<ISolution> parsedSolutions);

            Assert.Same(parsedPuzzle, parsedSolutions[0].ParentPuzzle);
            Assert.Equal(puzzle, parsedPuzzle);
            Assert.Equal(solution, parsedSolutions[0]);
        }

        internal static IEnumerable<object[]> ParseXMLfilesWithSolutionData() {

            yield return new object[] {
                new int[][]{
                        new int[]{6},
                        new int[]{2,6},
                        new int[]{1,5},
                        new int[]{2},
                        new int[]{1},
                        new int[]{4,1,5},
                        new int[]{11,5},
                        new int[]{3,7,6},
                        new int[]{3,7,4,2},
                        new int[]{1,11,1,1},
                        new int[]{1,11,2},
                        new int[]{1,13,1},
                        new int[]{15,1},
                        new int[]{15,1},
                        new int[]{15,1},
                        new int[]{16,1,1},
                        new int[]{18,2},
                        new int[]{20},
                        new int[]{20},
                        new int[]{19},
                        new int[]{17},
                        new int[]{15},
                        new int[]{13},
                        new int[]{11},
                    new int[]{4,4}
                },
                new int[][]{
                        new int[]{9},
                        new int[]{2,8},
                        new int[]{3,10},
                        new int[]{2,12},
                        new int[]{2,14},
                        new int[]{19},
                        new int[]{20},
                        new int[]{20},
                        new int[]{1,19},
                        new int[]{3,19},
                        new int[]{21},
                        new int[]{1,19},
                        new int[]{2,2,16},
                        new int[]{3,1,17},
                        new int[]{3,5,14},
                        new int[]{3,4,9},
                        new int[]{2,4,7},
                        new int[]{2,2,1,7},
                        new int[]{1,2,4},
                        new int[]{2,4,4},
                        new int[]{3,5}
                },
                new int[][] {
                     new int[]{ -1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1,-1,-1},
                     new int[]{ -1,-1,-1,-1,-1,-1,-1,-1, 1, 1,-1,-1, 1, 1, 1, 1, 1, 1,-1,-1,-1},
                     new int[]{ -1,-1,-1,-1,-1,-1,-1,-1,-1, 1,-1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1},
                     new int[]{ -1,-1,-1,-1,-1,-1,-1,-1,-1, 1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
                     new int[]{ -1,-1,-1,-1,-1,-1,-1,-1,-1,-1, 1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
                     new int[]{ -1,-1,-1,-1, 1, 1, 1, 1,-1,-1, 1,-1, 1, 1, 1, 1, 1,-1,-1,-1,-1},
                     new int[]{ -1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1, 1, 1, 1, 1, 1,-1,-1},
                     new int[]{ -1, 1, 1, 1,-1, 1, 1, 1, 1, 1, 1, 1,-1,-1, 1, 1, 1, 1, 1, 1,-1},
                     new int[]{  1, 1, 1,-1,-1, 1, 1, 1, 1, 1, 1, 1,-1, 1, 1, 1, 1,-1,-1, 1, 1},
                     new int[]{  1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1, 1,-1,-1, 1},
                     new int[]{  1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1, 1, 1},
                     new int[]{  1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1, 1,-1},
                     new int[]{  1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1, 1,-1},
                     new int[]{  1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1, 1,-1},
                     new int[]{  1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1, 1},
                     new int[]{  1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1, 1,-1,-1, 1},
                     new int[]{  1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1, 1, 1},
                     new int[]{ -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                     new int[]{ -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                     new int[]{ -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1},
                     new int[]{ -1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1},
                     new int[]{ -1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1},
                     new int[]{ -1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1},
                     new int[]{ -1,-1,-1,-1,-1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,-1,-1,-1,-1,-1},
                     new int[]{ -1,-1,-1,-1,-1,-1, 1, 1, 1, 1,-1, 1, 1, 1, 1,-1,-1,-1,-1,-1,-1}
                },
                "webpbn034024.xml"
            };
        }

            [Theory]
            [MemberData(nameof(ParseXMLfilesNoSolutionData))]
            internal void ParseXMLfilesNoSolution(int[][] rows, int[][] columns, string fileName) {
                var filePath = @"FunctioningTests\Parser\Files\XML\" + fileName;
                Assert.True(File.Exists(filePath));

                var puzzle = Factory.CreatePuzzle(rows, columns);

                var parser = Factory.CreateFileParser();
                parser.ParseXMLFile(filePath, out IPuzzle parsedPuzzle, out  _);
                Assert.Equal(puzzle, parsedPuzzle);
            }

            internal static IEnumerable<object[]> ParseXMLfilesNoSolutionData() {

                yield return new object[] {
                new int[][]{
                    new int[]{4,4,4,2},
                    new int[]{4,4,4,2},
                    new int[]{ },
                    new int[]{4,4,4,1},
                    new int[]{4,4,4,1},
                    new int[]{ },
                    new int[]{4,4,4},
                    new int[]{4,4,4},
                    new int[]{ },
                    new int[]{1,4,4,4},
                    new int[]{1,4,4,4},
                    new int[]{ },
                    new int[]{2,4,4,4},
                    new int[]{2,4,4,4},
                    new int[]{ },
                    new int[]{3,4,4,3},
                    new int[]{3,4,4,3},
                    new int[]{ },
                    new int[]{4,4,4,2},
                    new int[]{4,4,4,2},
                },
                new int[][]{
                      new int[]{2,2,2,2,2},
                        new int[]{2,2,2,2,2},
                        new int[]{2,2,2,2,2},
                        new int[]{2,2,2,2,2},
                        new int[]{2,2,2,2},
                        new int[]{2,2,2,2},
                        new int[]{2,2,2,2,2},
                        new int[]{2,2,2,2,2},
                        new int[]{2,2,2,2,2},
                        new int[]{2,2,2,2,2},
                        new int[]{2,2,2,2},
                        new int[]{2,2,2,2},
                        new int[]{2,2,2,2,2},
                        new int[]{2,2,2,2,2},
                        new int[]{2,2,2,2,2},
                        new int[]{2,2,2,2,2},
                        new int[]{2,2,2,2},
                        new int[]{2,2,2,2},
                        new int[]{2,2,2,2,2},
                        new int[]{2,2,2,2,2}
                },
                "webpbn030110.xml"
            };
                #endregion
            }
    }
}
