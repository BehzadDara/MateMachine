using MateMachine.API.Models;

namespace MateMachine.API;

public class CurrencyConverter : ICurrencyConverter
{
    private readonly Graph Graph = new();

    public void ClearConfiguration()
    {
        Graph.AdjacencyList.Clear();
    }

    public void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates)
    {
        foreach (var conversionRate in conversionRates)
        {
            Graph.AddEdge(conversionRate.Item1, conversionRate.Item2, conversionRate.Item3);
            Graph.AddEdge(conversionRate.Item2, conversionRate.Item1, 1/conversionRate.Item3);
        }
    }

    public double Convert(string fromCurrency, string toCurrency, double amount)
    {
        var shortestPath = Graph.ShortestPath(fromCurrency, toCurrency);

        var result = amount;
        for (var i = 0; i < shortestPath.Count - 1; i++)
        {
            var edge = Graph.AdjacencyList[shortestPath[i]].First(edge => edge.Destination == shortestPath[i+1]);
            result *= edge.Amount;
        }
        return result;
    }
}
