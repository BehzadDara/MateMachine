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
}
