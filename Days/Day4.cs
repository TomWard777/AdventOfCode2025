namespace AdventOfCode2025;

public class Day4
{
    private readonly int _m;
    private readonly int _n;
    private readonly char[][] _mat;
    private IEnumerable<string> _input;

    public Day4()
    {
        _input = FileParser.ReadInputFromFile("Day4.txt");
        ///_input = FileParser.ReadInputFromFile("Test4.txt");

        (_m, _n, _mat) = Matrices.ReadToMatrixTuple(_input);
    }

    public void Run()
    {
        var accessiblePlaces = new List<(int, int)>();
        var total = 0;

        do
        {
            accessiblePlaces = GetAccessiblePlaces();
            total += accessiblePlaces.Count;

            foreach (var p in accessiblePlaces)
            {
                _mat[p.Item1][p.Item2] = '.';
            }
        }
        while (accessiblePlaces.Count > 0);

        Matrices.Draw(_m, _n, _mat);

        Console.WriteLine($"\nFinal result = {total}");
    }

    public List<(int, int)> GetAccessiblePlaces()
    {
        var accessiblePlaces = new List<(int, int)>();

        for (int i = 0; i < _m; i++)
        {
            for (int j = 0; j < _n; j++)
            {
                if (_mat[i][j] == '@')
                {
                    var adjacent = Matrices.GetAdjacentPlaces(_m, _n, i, j);

                    var ct = 0;
                    foreach (var p in adjacent)
                    {
                        if (_mat[p.Item1][p.Item2] == '@')
                        {
                            ct++;
                        }
                    }

                    if (ct < 4)
                    {
                        accessiblePlaces.Add((i, j));
                    }
                }
            }
        }

        return accessiblePlaces;
    }
}