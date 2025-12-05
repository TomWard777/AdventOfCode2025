namespace AdventOfCode2025;

public class Day5
{
    public void Run()
    {
        var reader = FileParser.GetStreamReaderFromFile("Day5.txt");
        var ranges = new List<(long, long)>();
        var values = new List<long>();

        while (true)
        {
            var line = reader.ReadLine();

            if (string.IsNullOrWhiteSpace(line))
            {
                break;
            }

            ranges.Add(GetRangeFromString(line));
        }

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();

            values.Add(long.Parse(line));
        }

        var freshValues = values
        .Where(v => IsValueFresh(v, ranges))
        .ToArray();

        Console.WriteLine($"\nFinal result = {freshValues.Count()}");
    }

    public bool IsValueFresh(long val, List<(long, long)> ranges)
    {
        foreach (var range in ranges)
        {
            if (IsValueInRange(val, range))
            {
                return true;
            }
        }

        return false;
    }

    public bool IsValueInRange(long val, (long, long) range)
    {
        return val >= range.Item1 && val <= range.Item2;
    }

    public (long, long) GetRangeFromString(string str)
    {
        var arr = str.Split('-');
        return (long.Parse(arr[0]), long.Parse(arr[1]));
    }
}