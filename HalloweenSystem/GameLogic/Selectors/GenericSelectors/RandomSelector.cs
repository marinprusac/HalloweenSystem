using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors;

public class RandomSelector<T>(Limit<T> limit, Selector<T> nestedSelector) : Selector<T> where T : GameObject
{
	public override IEnumerable<T> Evaluate(Context context)
	{
		var gameObjects = nestedSelector.Evaluate(context);
		return limit.Evaluate(gameObjects);
	}
}