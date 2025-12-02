namespace AdventOfCode2025;

public class Day1
{
    public void Run()
    {
        var input = FileParser.ReadInputFromFile("Day1.txt");

        var moves = input.Select(x => ReadLine(x))
            .ToArray();

        var position = 50;
        var ct = 0;

        foreach (var (dir, steps) in moves)
        {
            // Take account of starting on zero then turning left
            if(position == 0 && dir == "L")
            {
                ct -= 1;
            }

            if (dir == "R")
            {
                position += steps;
            }
            else if (dir == "L")
            {
                position -= steps;
            }

            while (position < 0)
            {
                ct += 1;
                position += 100;
            };

            while (position > 100)
            {
                position -= 100;
                ct += 1;
            }

            if (position == 0 || position == 100)
            {
                position = 0;
                ct++;
            }

            Console.WriteLine(position);
            ////Console.WriteLine(ct);
        }

        Console.WriteLine($"\nFinal result = {ct}");
    }

    public (string, int) ReadLine(string line)
    {
        return (line.Substring(0, 1), int.Parse(line.Substring(1)));
    }
}