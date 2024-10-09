using HalloweenSystem.GameLogic.PlayerSelectors;
using HalloweenSystem.GameLogic.Utilities;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.PlayerSelectors;

public class RandomPlayerSelector(Limit<Player> limit, PlayerSelector nestedSelector) : PlayerSelector
{
	public override IEnumerable<Player> Evaluate(Context context, Player? operatedPlayer = null)
	{
		var players = nestedSelector.Evaluate(context, operatedPlayer);
		return limit.Evaluate(players);
	}
}