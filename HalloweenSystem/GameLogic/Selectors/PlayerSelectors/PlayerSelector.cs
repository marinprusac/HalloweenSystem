using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.PlayerSelectors;

public abstract class PlayerSelector
{

	public abstract IEnumerable<Player> Evaluate(Context context, Player? operatedPlayer = null);

}