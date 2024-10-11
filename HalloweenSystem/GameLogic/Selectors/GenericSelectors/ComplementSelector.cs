using System.Collections.Generic;
using System.Linq;
using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors;

public class ComplementSelector<T>(Selector<T> nestedSelector) : Selector<T> where T : GameObject, new()
{
	public override IEnumerable<T> Evaluate(Context context)
	{
		var gameObjects = nestedSelector.Evaluate(context);
		return GameObject.Complement<T>(gameObjects, context).Cast<T>();
	}
}