using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors;

public class AllSelector<T>(IEnumerable<Selector<T>> nestedSelectors) : Selector<T> where T : GameObject, new()
{
	public override IEnumerable<T> Evaluate(Context context)
	{
		var evaluations = nestedSelectors.Select(selector => selector.Evaluate(context)).ToList();
		var result = GameObject.Intersect<T>(evaluations);
		return result.Cast<T>();
	}
}