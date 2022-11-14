using System.Collections.Generic;

namespace PuzzleSolvers.Nanogram {
    public interface IFileParser {
        IPuzzle ParseStandardFile(string filePath, char cluesSeparator = ',');
        void ParseXMLFile(string filePath, out IPuzzle puzzle, out List<ISolution> solutions);
        void ParseXMLFileContent(string xmlFileContent, out IPuzzle puzzle, out List<ISolution> solutions);
    }
}