namespace MateMachine.API.Models;

public class Edge(Vertex destination, double amount)
{
    public Vertex Destination = destination;
    public double Amount = amount;
}
