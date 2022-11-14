using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml.Linq;

namespace PuzzleSolvers.Nanogram {
    internal class FileParser : IFileParser {


        public void ParseXMLFile(string filePath, out IPuzzle puzzle, out List<ISolution> solutions) {
            ParseXMLdocument(XElement.Load(filePath), out puzzle, out solutions);
        }
        public void ParseXMLFileContent(string xmlFileContent, out IPuzzle puzzle, out List<ISolution> solutions) {
            ParseXMLdocument(XElement.Parse(xmlFileContent), out puzzle, out solutions);
        }

        private void ParseXMLdocument(XElement xmlDoc, out IPuzzle puzzle, out List<ISolution> solutions) {
            var xmlPuzzle = xmlDoc.Descendants("puzzle").First();
            var xmlSolutions = xmlPuzzle.Descendants("solution");

            //Parse puzzle
            int[][] rowsClues = xmlPuzzle.Elements("clues").First(clue => clue.Attribute("type").Value == "rows")
                .Elements("line").Select(line => line.Elements("count").Select(count => ((int)count)).ToArray()).ToArray();
            int[][] columnsClues = xmlPuzzle.Elements("clues").First(clue => clue.Attribute("type").Value == "columns")
                .Elements("line").Select(line => line.Elements("count").Select(count => ((int)count)).ToArray()).ToArray();
            puzzle = Factory.CreatePuzzle(rowsClues, columnsClues);

            //Parse solutions
            solutions = xmlSolutions.Count() == 0 ? null : new List<ISolution>();
            foreach (var xmlSol in xmlSolutions) {
                var stringSol = ((string)xmlSol.Descendants("image").First()).Trim().Replace("|", "");
                int[][] gridSolution = stringSol.Split('\n')
                    .Select(line => line.Select(c => c == 'X' ? 1 : -1).ToArray()).ToArray();
                solutions.Add(Factory.CreateSolution(puzzle, gridSolution));
            }
        }

        public IPuzzle ParseStandardFile(string filePath, char cluesSeparator = ',') {
            var lines = File.ReadAllLines(filePath);

            int rowsLeft = -1, colsLeft = -1;
            var rows = new List<int[]>();
            var columns = new List<int[]>();

            foreach (var line in lines) {
                if (!(line.StartsWith("#") || line.StartsWith("(") || string.IsNullOrWhiteSpace(line))) {
                    if (rowsLeft == -1 || colsLeft == -1) {
                        rowsLeft = int.Parse(line.Split(cluesSeparator)[0]);
                        colsLeft = int.Parse(line.Split(cluesSeparator)[1]);
                    } else {
                        var parsedLine = line.Split(cluesSeparator).Select(x => int.Parse(x)).ToArray();

                        if (rowsLeft > 0) {
                            rows.Add(parsedLine);
                            rowsLeft--;

                        } else if (colsLeft > 0) {
                            columns.Add(parsedLine);
                            colsLeft--;
                        }
                    }
                }
            }

            if (rowsLeft != 0 || colsLeft != 0) {
                throw new InvalidDataException("Number of rows or columns doesn't match number of clue lines in file");
            }

            return Factory.CreatePuzzle(rows.ToArray(), columns.ToArray());
        }

        
    }
}
