using HalloweenSystem.GameLogic.PlayerSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.PlayerSelectors;

public class ExtractPlayerSelector(TagType tagType, PlayerSelector nestedSelector) : PlayerSelector
{
	public override IEnumerable<Player> Evaluate(Context context, Player? operatedPlayer = null)
	{
		var players = nestedSelector.Evaluate(context, operatedPlayer);
		var resultingPlayers = new List<Player>();
		foreach (var player in players)
		{
			if (!player.GetTagOfType(tagType, out var tag)) continue;
			var playersWithTag = tag?.PlayerParameters;
			if (playersWithTag != null)
			{
				resultingPlayers.AddRange(playersWithTag);
			}
		}

		return resultingPlayers;
	}
}