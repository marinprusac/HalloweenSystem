using System.Collections.Generic;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors;

public class RandomSelector<T>(string amount, Selector<T>? nestedSelector=null) : Selector<T> where T : GameObject, new()
{
	private Selector<T>? _nestedSelector = nestedSelector;

	public override IEnumerable<T> Evaluate(Context context)
	{
		_nestedSelector ??= new AllSelector<T>();
		var gameObjects = _nestedSelector.Evaluate(context);
		var limit = new Limit<T>(amount);
		return limit.Evaluate(gameObjects);
	}
}