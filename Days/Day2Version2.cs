namespace AdventOfCode2025;

public class Day2Version2
{
    public void Run()
    {
        var input = FileParser.ReadInputFromFile("Day2.txt").First();

        var ranges = input.Split(',')
            .Select(str => str.Split("-").Select(x => long.Parse(x)).ToArray())
            .ToList();

        var results = new List<long>();

        foreach (var v in ranges)
        {
            results.AddRange(GetAnswersInRange(v));
        }

        // Print results
        foreach (var num in results)
        {
            Console.WriteLine(num);
        }

        Console.WriteLine($"\nFinal result = {results.Sum()}");
    }

    public IList<long> GetAnswersInRange(long[] range)
    {
        var (a, b) = (range[0], range[1]);
        var n = a;
        var results = new List<long>();

        while (n <= b)
        {
            if (TestString2(n.ToString()))
            {
                results.Add(n);
            }

            n++;
        }

        return results;
    }

    public bool TestString(string str)
    {
        var k = str.Length;

        if (k % 2 == 1)
        {
            return false;
        }

        var half = str.Substring(0, k / 2);

        return str == half + half;
    }

    public bool TestString2(string str)
    {
        var k = 1;

        while (k <= str.Length / 2)
        {
            var subStr = str.Substring(0, k);
            var test = subStr;

            while (test.Length < str.Length)
            {
                test += subStr;
            }

            if (test == str)
            {
                return true;
            }

            k++;
        }

        return false;
    }
}