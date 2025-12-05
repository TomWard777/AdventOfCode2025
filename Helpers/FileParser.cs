namespace AdventOfCode2025;

public static class FileParser
{
    public static IEnumerable<string> ReadInputFromFile(string fileName)
    {
        var path = GetFilePath(fileName);
        return File.ReadLines(path);
    }

    public static StreamReader GetStreamReaderFromFile(string fileName)
    {
        var path = GetFilePath(fileName);
        return new StreamReader(path);
    }

    private static string GetFilePath(string fileName)
    {
        return Path.Combine(Directory.GetCurrentDirectory(), "PuzzleInput", fileName);
    }
}
