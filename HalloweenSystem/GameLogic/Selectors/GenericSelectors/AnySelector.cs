using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors;

public class AnySelector<T>(IEnumerable<Selector<T>> nestedSelectors) : Selector<T> where T : GameObject, new()
{
	public override IEnumerable<T> Evaluate(Context context)
	{
		
		var evaluations = nestedSelectors.Select(selector => selector.Evaluate(context)).ToList();
		var result = GameObject.Union<T>(evaluations);
		return result.Cast<T>();
	}
}