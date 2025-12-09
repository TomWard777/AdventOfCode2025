using System.Drawing;
using Microsoft.VisualBasic;

namespace AdventOfCode2025;

public class Day9
{
    public void Run()
    {
        var input = FileParser.ReadInputFromFile("Day9.txt");
        //var input = FileParser.ReadInputFromFile("Test9.txt");

        var points = input.Select(str => str.Split(','))
        .Select(v => (long.Parse(v[0]), long.Parse(v[1])))
        .ToArray();

        var checkedPoints = new List<(long, long)>();

        // IsRectangleViable((2L, 5L), (11L, 7L), points);
        // throw new Exception();

        var rectangles = GetRectangles(points).OrderByDescending(r => r.Area);

        Console.WriteLine($"Number of rectangles = {rectangles.Count()}");

        var ct = 1;

        foreach (var rect in rectangles)
        {
            var (p, q) = (rect.Point1, rect.Point2);

            if (IsRectangleViable(p, q, points))
            {
                PrintPair(p);
                Console.Write(" and ");
                PrintPair(q);
                Console.WriteLine("");
                Console.WriteLine($"AREA = {rect.Area}");
                break;
            }

            Console.WriteLine(ct);
            ct++;
        }
    }

    public bool IsRectangleViable(
        (long, long) pair1,
        (long, long) pair2,
        IEnumerable<(long, long)> points)
    {
        var (x1, y1) = pair1;
        var (x2, y2) = pair2;

        if (x1 == x2 || y1 == y2)
        {
            // Rectangle is a strip of width 1 - always viable.
            return true;
        }

        var corner1 = (x1, y2);
        var corner2 = (x2, y1);

        var allCorners = new List<(long, long)>();
        allCorners.Add(pair1);
        allCorners.Add(pair2);
        allCorners.Add(corner1);
        allCorners.Add(corner2);

        var xMin = Math.Min(x1, x2);
        var xMax = Math.Max(x1, x2);
        var yMin = Math.Min(y1, y2);
        var yMax = Math.Max(y1, y2);

        for (var x = xMin; x <= xMax; x++)
        {
            for (var y = yMin; y <= yMax; y++)
            {
                if (!allCorners.Contains((x, y)) && points.Contains((x, y)))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public bool IsPointViable(
        (long, long) pair,
        List<(long, long)> points)
    {
        if (points.Contains(pair))
        {
            // Point is in the list of red points.
            return true;
        }

        // UNFINISHED
        return false;
    }

    public List<Rectangle> GetRectangles(IEnumerable<(long, long)> points)
    {
        var checkedPoints = new List<(long, long)>();
        var rectangleList = new List<Rectangle>();

        foreach (var p in points)
        {
            checkedPoints.Add(p);

            foreach (var q in points)
            {
                if (checkedPoints.Contains(q))
                {
                    continue;
                }

                var area = GetArea(p, q);

                var rectangle = new Rectangle
                {
                    Point1 = p,
                    Point2 = q,
                    Area = GetArea(p, q)
                };

                rectangleList.Add(rectangle);
            }
        }

        return rectangleList;
    }

    public long GetArea((long, long) p, (long, long) q)
    {
        var xDist = Math.Abs(p.Item1 - q.Item1) + 1;
        var yDist = Math.Abs(p.Item2 - q.Item2) + 1;
        return xDist * yDist;
    }

    public void PrintPair((long, long) pair)
    {
        var (a, b) = pair;
        Console.Write($"({a}, {b})");
    }
}

public class Rectangle
{
    public (long, long) Point1 { get; set; }
    public (long, long) Point2 { get; set; }
    public long Area { get; set; }
}