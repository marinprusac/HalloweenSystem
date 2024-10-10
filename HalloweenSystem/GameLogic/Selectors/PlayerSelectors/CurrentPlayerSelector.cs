using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.PlayerSelectors;

public class CurrentPlayerSelector : Selector<Player>
{
	public override IEnumerable<Player> Evaluate(Context context)
	{
		return context.CurrentPlayer == null ? [] : [context.CurrentPlayer];
	}
}