namespace AdventOfCode2025;

public class Day8
{
    public void Run()
    {
        var input = FileParser.ReadInputFromFile("Day8.txt");
        ///var input = FileParser.ReadInputFromFile("Test8.txt");

        ////var numberOfPairsToConnect = 1000;
        ////var numberOfPairsToConnect = 10;

        var points = input.Select(ReadStringToPoint).ToArray();

        var n = points.Count();
        var distanceDictionary = new Dictionary<(int, int), double>();

        for (var j = 0; j < n; j++)
        {
            for (var k = j; k < n; k++)
            {
                if (k != j)
                {
                    var v = points[j];
                    var w = points[k];
                    distanceDictionary.Add((j, k), Dist(v, w));
                }
            }
        }

        // var closestPairs = distanceDictionary
        //     .OrderBy(k => k.Value)
        //     .Select(x => x.Key)
        //     .Take(numberOfPairsToConnect)
        //     .ToList();

        // var components = GetConnectedComponents(closestPairs);

        var orderedEdges = distanceDictionary
            .OrderBy(k => k.Value)
            .Select(x => x.Key)
            .ToArray();

        var totalOrder = GetVertices(orderedEdges).Count;
        var startTake = 0;

        for (var take = 11; take < distanceDictionary.Keys.Count; take++)
        {
            var edges = orderedEdges.Take(take).ToList();
            var order = GetVertices(edges).Count;
            if (order == totalOrder)
            {
                startTake = take;
                break;
            }
        }

        for (var take = startTake; take < distanceDictionary.Keys.Count; take++)
        {
            var edges = orderedEdges.Take(take).ToList();
            var components = GetConnectedComponents(edges);

            if (components.Count == 1)
            {
                var finalEdge = edges.Last();
                var v = points[finalEdge.Item1];
                var w = points[finalEdge.Item2];

                Console.WriteLine($"ANSWER = {v[0] * w[0]}");
                break;
            }
        }
        // foreach (var p in closestPairs)
        // {
        //     var v = points[p.Item1];
        //     var w = points[p.Item2];
        //     Console.WriteLine($"({v[0]},{v[1]},{v[2]}) ({w[0]},{w[1]},{w[2]}) Distance = {distanceDictionary[p]}");
        // }

        // foreach (var p in closestPairs)
        // {
        //     Console.WriteLine(p);
        // }

        // foreach (var c in components)
        // {
        //     foreach (var v in c)
        //     {
        //         Console.Write($"{v} ");
        //     }
        //     Console.WriteLine();
        // }

        // var componentSizes = components.Select(c => c.Count);
        // var result = Maths.Product(componentSizes.OrderByDescending(k => k).Take(3));

        // Console.WriteLine($"\nFinal result = {result}");
    }

    public List<List<int>> GetConnectedComponents(List<(int, int)> edges)
    {
        var vertices = GetVertices(edges);
        var components = new List<List<int>>();

        var ungroupedVertices = vertices;

        while (ungroupedVertices.Count > 0)
        {
            var v = ungroupedVertices.First();
            var component = new List<int>() { v };

            int count1;
            int count2;

            do
            {
                count1 = component.Count;

                var neighbourhood = GetNeighbourhood(component, edges);
                component.AddRange(neighbourhood);
                component = component.Distinct().ToList();

                count2 = component.Count;
            } while (count2 != count1);

            components.Add(component);
            ungroupedVertices = ungroupedVertices.Except(component).ToList();
        }

        return components;
    }

    public List<int> GetNeighbourhood(List<int> vertexSet, List<(int, int)> edges)
    {
        var neighbourhood = new List<int>();

        foreach (var v in vertexSet)
        {
            var vert1 = edges.Where(e => e.Item1 == v).Select(e => e.Item2);
            var vert2 = edges.Where(e => e.Item2 == v).Select(e => e.Item1);
            neighbourhood.AddRange(vert1);
            neighbourhood.AddRange(vert2);
        }

        return neighbourhood.Distinct().ToList();
    }

    public List<int> GetVertices(IEnumerable<(int, int)> edges)
    {
        var vertices1 = edges.Select(e => e.Item1).ToList();
        var vertices2 = edges.Select(e => e.Item2).ToList();
        vertices1.AddRange(vertices2);

        return vertices1
        .Distinct()
        .ToList();
    }

    public double Dist(long[] v, long[] w)
    {
        var x = v[0] - w[0];
        var y = v[1] - w[1];
        var z = v[2] - w[2];
        return Math.Sqrt(x * x + y * y + z * z);
    }

    public long[] ReadStringToPoint(string str)
    {
        return str.Split(',')
            .Select(x => long.Parse(x))
            .ToArray();
    }
}