using System.Collections.Generic;
using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors;

public class EmptySelector<T> : Selector<T> where T : GameObject
{
	public override IEnumerable<T> Evaluate(Context context)
	{
		return [];
	}
}