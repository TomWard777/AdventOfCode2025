namespace AdventOfCode2025;

public class Day5Part2
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

        var count = 0;
        var newCount = 0;

        //// Initial step to ensure more than one reduction. 
        //// I don't think this would work in all cases...
        ranges = Reduce(ranges);

        do
        {
            count = ranges.Count;
            ranges = Reduce(ranges);

            newCount = ranges.Count;
        } while (newCount != count);

        var total = ranges.Select(r => GetRangeLength(r)).Sum();

        Console.WriteLine($"\nFinal result = {total}");
    }

    public List<(long, long)> Reduce(List<(long, long)> rangeList)
    {
        var results = rangeList;

        foreach (var r in rangeList)
        {
            results = MergeRangeList(r, results);
        }

        return results.Distinct().ToList();
    }

    public List<(long, long)> MergeRangeList((long, long) inputRange, List<(long, long)> rangeList)
    {
        var results = new List<(long, long)>();

        foreach (var r in rangeList)
        {
            if (inputRange != r && DoRangesIntersect(inputRange, r))
            {
                results.Add(MergeRanges(inputRange, r));
            }
            else
            {
                results.Add(r);
            }
        }

        return results;
    }

    public (long, long) MergeRanges((long, long) rangeA, (long, long) rangeB)
    {
        var (a, b) = rangeA;
        var (c, d) = rangeB;

        return (Math.Min(a, c), Math.Max(b, d));
    }

    public bool DoRangesIntersect((long, long) rangeA, (long, long) rangeB)
    {
        var (a, b) = rangeA;
        var (c, d) = rangeB;

        if (a <= c)
        {
            return b >= c;
        }
        else
        {
            return d >= a;
        }
    }

    public long GetRangeLength((long, long) range) => range.Item2 - range.Item1 + 1;

    public (long, long) GetRangeFromString(string str)
    {
        var arr = str.Split('-');
        return (long.Parse(arr[0]), long.Parse(arr[1]));
    }
}