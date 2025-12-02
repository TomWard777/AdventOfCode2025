namespace AdventOfCode2025;

public class Day2
{
    public void Run()
    {
        //// Flawed implementation - better to do this with strings.
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

        var digA = Maths.GetNumberOfDigits(a);
        var digB = Maths.GetNumberOfDigits(b);

        var kLower = digA / 2;

        if (digA % 2 == 1)
        {
            kLower++;
        }

        var kUpper = digB / 2;

        var kRange = Enumerable.Range(kLower, kUpper);
        var results = new List<long>();

        foreach (var k in kRange)
        {
            var r = Maths.LongPower(10, k - 1);
            var multiplier = Maths.LongPower(10, k) + 1;

            Console.WriteLine($"Testing r = {r}, mult = {multiplier} against range {a} - {b}");
            
            while (r * multiplier <= b && r < Maths.LongPower(10, k))
            {
                if (r * multiplier >= a)
                {
                    results.Add(r * multiplier);
                }
                r++;
            }
        }

        return results;
    }
}