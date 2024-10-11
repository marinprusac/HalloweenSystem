using System.Collections.Generic;
using System.Linq;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.PlayerSelectors;

public class RemoveCurrentPlayerSelector(Selector<Player> playerSelector) : Selector<Player>
{
	public override IEnumerable<Player> Evaluate(Context context)
	{
		var currentPLayer = context.CurrentPlayer;
		var players = playerSelector.Evaluate(context);
		var result = players.Where(player => player != currentPLayer);
		return result;
	}
}