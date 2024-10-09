using HalloweenSystem.GameLogic.PlayerSelectors;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.Selectors.PlayerSelectors;

public class CurrentPlayerSelector : PlayerSelector
{
	public override IEnumerable<Player> Evaluate(Context context, Player? operatedPlayer = null)
	{
		return operatedPlayer == null ? [] : [operatedPlayer];
	}
}