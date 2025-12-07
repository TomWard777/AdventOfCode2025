namespace AdventOfCode2025;

public class Day7Part2
{
    private readonly int _m;
    private readonly int _n;
    private readonly char[][] _mat;
    private IEnumerable<string> _input;
    private Dictionary<(int, int), long> _numberOfPathsDictionary;

    public Day7Part2()
    {
        _input = FileParser.ReadInputFromFile("Day7.txt");
        ////_input = FileParser.ReadInputFromFile("Test7.txt");

        (_m, _n, _mat) = Matrices.ReadToMatrixTuple(_input);
        _numberOfPathsDictionary = new Dictionary<(int, int), long>();
    }

    public void Run()
    {
        PopulateDictionary();

        var (i, j) = Matrices.GetUniqueCharacterPosition(_mat, 'S');
        var topSplitter = (i + 2, j);

        Console.WriteLine($"\nFinal result = {_numberOfPathsDictionary[topSplitter]}");
    }

    public void PopulateDictionary()
    {
        var k = _m - 2;

        for (var i = _m - 2; i > 0; i = i - 2)
        {
            for (var j = 0; j < _n; j++)
            {
                if (_mat[i][j] == '^')
                {
                    var num = GetNumberOfPathsFromSplitter((i, j));
                    _numberOfPathsDictionary.Add((i, j), num);

                    Console.WriteLine($"Paths from ({i}, {j}) = {num}");
                }
            }
        }
    }

    public long GetNumberOfPathsFromSplitter((int, int) splitterPosition)
    {
        var (i, j) = splitterPosition;

        if (_mat[i][j] != '^')
        {
            throw new Exception("Point is not a splitter");
        }

        if (i == _m - 2)
        {
            return 2;
        }

        var leftSplitterBelow = GetFirstSplitterBelow((i, j - 1));
        var rightSplitterBelow = GetFirstSplitterBelow((i, j + 1));

        var result = 0L;

        if (leftSplitterBelow.Item1 == -1)
        {
            result++;
        }
        else
        {
            result += _numberOfPathsDictionary[leftSplitterBelow];
        }

        if (rightSplitterBelow.Item1 == -1)
        {
            result++;
        }
        else
        {
            result += _numberOfPathsDictionary[rightSplitterBelow];
        }

        return result;
    }

    public (int, int) GetFirstSplitterBelow((int, int) start)
    {
        var (i, j) = start;

        do
        {
            i++;

            if (i == _m)
            {
                return (-1, -1);
            }

        } while (_mat[i][j] != '^');

        return (i, j);
    }
}