namespace AdventOfCode2025;

public class Day7
{
    private readonly int _m;
    private readonly int _n;
    private readonly char[][] _mat;
    private IEnumerable<string> _input;

    private int _numberOfSplits;

    public Day7()
    {
        _input = FileParser.ReadInputFromFile("Day7.txt");
        ///_input = FileParser.ReadInputFromFile("Test7.txt");

        (_m, _n, _mat) = Matrices.ReadToMatrixTuple(_input);
        _numberOfSplits = 0;
    }

    public void Run()
    {
        var start = Matrices.GetUniqueCharacterPosition(_mat, 'S');

        var beam = new List<(int, int)>();
        var totalBeam = new List<(int, int)>();

        beam.Add(start);
        _numberOfSplits = 0;

        while (beam.Count > 0)
        {
            totalBeam.AddRange(beam);
            beam = GetNextBeamPositions(beam);
        }

        Matrices.DrawSubset(_m, _n, _mat, totalBeam);

        Console.WriteLine($"\nFinal result = {_numberOfSplits}");
    }

    public List<(int, int)> GetNextBeamPositions(List<(int, int)> beam)
    {
        var nextBeam = new List<(int, int)>();

        foreach (var (i, j) in beam)
        {
            if (Matrices.IsPointOutsideMatrix((i + 1, j), _m, _n))
            {
                continue;
            }

            var charBelow = _mat[i + 1][j];

            if (charBelow == '.')
            {
                nextBeam.Add((i + 1, j));
            }
            else if (charBelow == '^')
            {
                _numberOfSplits++;
                nextBeam.Add((i + 1, j - 1));
                nextBeam.Add((i + 1, j + 1));
            }
        }

        return nextBeam.Distinct().ToList();
    }
}