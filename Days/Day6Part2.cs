namespace AdventOfCode2025;

public class Day6Part2
{
    public void Run()
    {
        var input = FileParser.ReadInputFromFile("Day6.txt");
        ///var input = FileParser.ReadInputFromFile("Test6.txt");

        var (mTemp, nTemp, matTemp) = Matrices.ReadToMatrixTuple(input);

        var (m, n, mat) = Matrices.GetTransposeMatrixTuple(mTemp, nTemp, matTemp);

        //Matrices.Draw(m, n, mat);

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
                total += GetCalculationResult(operation, buffer);
                buffer.Clear();
            }
            else
            {
                buffer.Add(long.Parse(numberStr));
            }

            i++;
        }

        // No blank line at the end, so we add a result to the total one more time
        total += GetCalculationResult(operation, buffer);

        return total;
    }

    public long GetCalculationResult(char operation, List<long> buffer)
    {
        switch (operation)
        {
            case '+':
                return buffer.Sum();
            case '*':
                return Maths.Product(buffer);
            default:
                throw new Exception($"Error, operation is read as: {operation}");
        }
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