using System.Collections.Generic;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors;

public class RandomSelector<T>(string amount, Selector<T> nestedSelector) : Selector<T> where T : GameObject
{
	public override IEnumerable<T> Evaluate(Context context)
	{
		var gameObjects = nestedSelector.Evaluate(context);
		var limit = new Limit<T>(amount);
		return limit.Evaluate(gameObjects);
	}
}