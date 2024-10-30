namespace HalloweenSystem.GameLogic.Utilities;

public class Statistics
{
	private List<int> combinations { get; } = [];

	public static int Factorial(int n)
	{
		if (n == 0) return 1;
		return n * Factorial(n - 1);
	}
	
	public void LogCombination(int count)
	{
		combinations.Add(count);
	}
	
	
	public int GetChoiceCount()
	{
		return combinations.Count;
	}
	
	public int GetCombinationCount()
	{
		return combinations.Aggregate(1, (prev, incoming) =>  prev*incoming );
	}
}