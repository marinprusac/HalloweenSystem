using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors;

public class EverySelector<T> : Selector<T> where T : GameObject, new()
{
	public override IEnumerable<T> Evaluate(Context context)
	{
		var gameObjects = GameObject.Everything<T>(context);
		return gameObjects.Cast<T>();
	}
}