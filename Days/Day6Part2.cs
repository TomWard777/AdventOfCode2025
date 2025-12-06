namespace AdventOfCode2025;

public class Day6Part2
{
    public void Run()
    {
        ////var input = FileParser.ReadInputFromFile("Day6.txt");
        var input = FileParser.ReadInputFromFile("Test6.txt");

        var (mTemp, nTemp, matTemp) = Matrices.ReadToMatrixTuple(input);

        var (m, n, mat) = Matrices.GetTransposeMatrixTuple(mTemp, nTemp, matTemp);

        Matrices.Draw(m, n, mat);

        Console.WriteLine($"\nFinal result = {ComputeAnswer(m, n, mat)}");
    }

    public long ComputeAnswer(int m, int n, char[][] mat)
    {
        var buffer = new List<long>();

        var i = 0;
        var total = 0L;
        char operation = 'E';

        while (i < m)
        {
            var newOp = mat[i][n - 1];

            if (newOp != ' ')
            {
                operation = newOp;
            }

            var numberStr = new string(mat[i].Take(n - 1).ToArray());

            if (string.IsNullOrWhiteSpace(numberStr))
            {
                switch (operation)
                {
                    case '+':
                    Console.WriteLine(buffer.Sum());
                        total += buffer.Sum();
                        break;
                    case '*':
                    Console.WriteLine(Maths.Product(buffer));
                        total += Maths.Product(buffer);
                        break;
                    default:
                        throw new Exception($"Error, operation is read as: {operation}");
                }

                buffer.Clear();
            }
            else
            {
                // Console.WriteLine("Add to buffer:");
                // Console.WriteLine(numberStr);

                buffer.Add(long.Parse(numberStr));
            }

            i++;
        }

//// THERE'S NO BLANK ROW AT THE END, YOU NEED TO GET THE TOTAL FROM THE BUFFER ONE MORE TIME
        return total;
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