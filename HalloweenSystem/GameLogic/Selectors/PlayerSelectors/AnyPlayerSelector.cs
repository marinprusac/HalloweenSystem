using HalloweenSystem.GameLogic.PlayerSelectors;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.PlayerSelectors;

public class AnyPlayerSelector(IEnumerable<PlayerSelector> playerExpressions) : PlayerSelector
{
	public override IEnumerable<Player> Evaluate(Context context, Player? operatedPlayer = null)
	{
		IEnumerable<Player>? players = null;
		foreach (var playerExpression in playerExpressions)
		{
			players = players == null ? playerExpression.Evaluate(context, operatedPlayer) : players.Union(playerExpression.Evaluate(context, operatedPlayer));
		}

		return players ?? throw new InvalidOperationException();
	}
}