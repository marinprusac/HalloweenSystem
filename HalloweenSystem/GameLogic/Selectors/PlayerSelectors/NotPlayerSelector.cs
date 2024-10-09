using HalloweenSystem.GameLogic.PlayerSelectors;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.PlayerSelectors;

public class NotPlayerSelector(PlayerSelector nestedSelector) : PlayerSelector
{
	public override IEnumerable<Player> Evaluate(Context context, Player? operatedPlayer = null)
	{
		return context.Players.Except(nestedSelector.Evaluate(context, operatedPlayer));
	}
}