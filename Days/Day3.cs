namespace AdventOfCode2025;

public class Day3
{
    public void Run()
    {
        var input = FileParser.ReadInputFromFile("Day3.txt");
        var total = 0L;
        var targetLength = 12;

        foreach (var line in input)
        {
            var score = GetScoreWithLength(line, targetLength);
            total += score;

            Console.WriteLine($"{line}");
            Console.WriteLine($"Scores {score}");
        }

        Console.WriteLine($"\nFinal result = {total}");
    }

    public long GetScoreWithLength(string line, int targetLength)
    {
        if (targetLength == 1)
        {
            var (val, _) = GetBestValueAndTail(line);

            return val;
        }

        var len = line.Length;
        var remainingLength = targetLength - 1;

        var head = line.Substring(0, len - remainingLength);
        var tail = line.Substring(len - remainingLength);

        var (bestVal, substr) = GetBestValueAndTail(head);

        return Maths.LongPower(10, remainingLength) * bestVal + GetScoreWithLength(substr + tail, remainingLength);
    }

    public long GetScore(string line)
    {
        var len = line.Length;

        var head = line.Substring(0, len - 1);
        var tail = line.Substring(len - 1);

        var (firstNumber, substr) = GetBestValueAndTail(head);
        var (secondNumber, _) = GetBestValueAndTail(substr + tail);

        return 10 * firstNumber + secondNumber;
    }

    public (long, string) GetBestValueAndTail(string str)
    {
        var number = 9;
        var len = str.Length;

        while (true)
        {
            var numberChar = number.ToString()[0];

            for (int i = 0; i < len; i++)
            {
                if (str[i] == numberChar)
                {
                    return (number, str.Substring(i + 1));
                }
            }

            number--;
        }
    }
}