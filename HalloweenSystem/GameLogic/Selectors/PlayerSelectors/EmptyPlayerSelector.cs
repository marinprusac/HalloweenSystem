using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.PlayerSelectors;

public class EmptyPlayerSelector : PlayerSelector
{
	public override IEnumerable<Player> Evaluate(Context context, Player? operatedPlayer = null)
	{
		return [];
	}
}