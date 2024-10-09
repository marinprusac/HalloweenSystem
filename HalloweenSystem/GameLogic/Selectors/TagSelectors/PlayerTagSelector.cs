using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.Selectors.TagSelectors;

public class PlayerTagSelector(Selector<Player> playerSelector, Selector<Tag> tagSelector) : Selector<Tag>
{
	public override IEnumerable<Tag> Evaluate(Context context)
	{
		var players = playerSelector.Evaluate(context);
		var tags = tagSelector.Evaluate(context);
		var playerTags = players.Select(player => player.AssignedTags.AsEnumerable());
		var joinedTags = GameObject.Union<Tag>(playerTags).Cast<Tag>();

		var result = joinedTags.Where(jt => tags.Any(jt.Covers));
		return result.Cast<Tag>();
	}
}