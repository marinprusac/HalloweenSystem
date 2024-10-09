using HalloweenSystem.GameLogic.PlayerSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.TagSelectors;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.Selectors.PlayerSelectors;

public class TagPlayerSelector(TagType tagTypeType, PlayerSelector? playerSelector=null, TagSelector? tagSelector=null)
	: PlayerSelector
{
	public override IEnumerable<Player> Evaluate(Context context, Player? operatedPlayer = null)
	{
		var players = playerSelector?.Evaluate(context, operatedPlayer) ?? [];
		var tags = tagSelector?.Evaluate(context, operatedPlayer).Select(at => at.Type) ?? [];
		var assignedTag = new AssignedTag(tagTypeType, players, tags);
		return context.Players.Where(player => player.GetQueriedTag(assignedTag, out _));
	}
}