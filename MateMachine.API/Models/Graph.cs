
namespace MateMachine.API.Models;

public class Graph
{
    public Dictionary<Vertex, List<Edge>> AdjacencyList = [];

    public void AddEdge(string source, string destination, double amount)
    {
        if (string.Equals(source, destination))
        {
            return;
        }

        var sourceVertex = new Vertex(source);
        var destinationVertex = new Vertex(destination);

        if (!AdjacencyList.TryGetValue(sourceVertex, out List<Edge>? edges))
        {
            edges = ([]);
            AdjacencyList[sourceVertex] = edges;
        }

        var edge = edges.FirstOrDefault(edge => edge.Destination == destinationVertex);
        if (edge is not null)
        {
            edge.Amount = amount;
        }
        else
        {
            edges.Add(new Edge(destinationVertex, amount));
        }
    }

    public List<Vertex> ShortestPath(string source, string destination)
    {
        try
        {
            var sourceVertex = new Vertex(source);
            var destinationVertex = new Vertex(destination);

            var queue = new Queue<Vertex>();
            var seen = new Dictionary<Vertex, bool>();
            var parent = new Dictionary<Vertex, Vertex?>();

            foreach (var vertex in AdjacencyList.Keys)
            {
                seen[vertex] = false;
                parent[vertex] = null;
            }

            queue.Enqueue(sourceVertex);
            seen[sourceVertex] = true;

            while (queue.Count > 0)
            {
                var currentVertex = queue.Dequeue();

                foreach (var edge in AdjacencyList[currentVertex])
                {
                    var neighbor = edge.Destination;
                    if (!seen[neighbor])
                    {
                        seen[neighbor] = true;
                        parent[neighbor] = currentVertex;
                        queue.Enqueue(neighbor);
                    }
                }
            }

            var shortestPath = new List<Vertex>();
            var current = destinationVertex;
            while (current is not null)
            {
                shortestPath.Add(current);
                current = parent[current];
            }
            if (shortestPath.Count == 1)
            {
                throw new Exception();
            }

            shortestPath.Reverse();

            return shortestPath;
        }
        catch
        {
            throw new Exception($"Can not convert from {source} to {destination}.");
        }
    }
}
