namespace MateMachine.API.Models;

public class Vertex(string name) : IEquatable<object>
{
    public string Currency = name;

    public override bool Equals(object? obj)
    {
        if (obj is Vertex vertex)
        {
            return vertex.Currency == Currency;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Currency.GetHashCode();
    }

    public static bool operator ==(Vertex obj1, Vertex obj2) => obj1.Equals(obj2);

    public static bool operator !=(Vertex obj1, Vertex obj2) => !(obj1 == obj2);
}