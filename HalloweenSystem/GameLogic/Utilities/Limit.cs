namespace HalloweenSystem.GameLogic.Utilities;

public class Limit<T>(string amount)
{
	public IEnumerable<T> Evaluate(IEnumerable<T> objects)
	{
		var enumerable = objects.ToList();
		var count = enumerable.Count;
		int selectedCount;
		if (amount.Contains('%'))
		{
			var success = int.TryParse(amount.Replace("%", ""), out var result);
			if(success) selectedCount = (int)Math.Ceiling(count * (result / 100f));
			else throw new ArgumentException();
		}
		else
		{
			var success = int.TryParse(amount, out var result);
			if (success) selectedCount = result;
			else throw new ArgumentException();
		}

		var random = new Random();
		var selected = enumerable.ToList();
		for(int i = 0; i< count-selectedCount; i++)
		{
			selected.RemoveAt(random.Next(selected.Count));
		}

		return selected;

	}
}