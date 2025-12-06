using System.Xml.Schema;

namespace AdventOfCode2025;

public class Day6
{
    public void Run()
    {
        var reader = FileParser.GetStreamReaderFromFile("Day6.txt");
        var rows = new List<string[]>();

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();

            rows.Add(line.Split(' ', StringSplitOptions.RemoveEmptyEntries));
        }

        var mat = rows.ToArray();
        var m = mat.Length;
        var n = mat[0].Length;

        var answer = 0L;

        for (var j = 0; j < n; j++)
        {
            var val = GetColumnValue(j, m, n, mat);
            answer += val;
        }

        Console.WriteLine($"\nFinal result = {answer}");
    }

    public long GetColumnValue(int colIndex, int m, int n, string[][] mat)
    {
        var operation = mat[m - 1][colIndex];

        long total = operation == "+" ? 0 : 1;

        for (var i = 0; i < m - 1; i++)
        {
            var num = long.Parse(mat[i][colIndex]);

            total = operation == "+" ? total + num : total * num;
        }

        return total;
    }
}