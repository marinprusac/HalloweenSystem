using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors;

public class EmptySelector<T> : Selector<T> where T : GameObject
{
	public override IEnumerable<T> Evaluate(Context context)
	{
		return [];
	}
}