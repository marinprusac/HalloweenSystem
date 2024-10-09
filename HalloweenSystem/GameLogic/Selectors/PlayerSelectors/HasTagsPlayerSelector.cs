using HalloweenSystem.GameLogic.Selectors.TagSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.Selectors.PlayerSelectors;

public class HasTagsPlayerSelector(Selector<Tag>? tagSelector=null, Selector<Player>? playerSelector=null)
	: Selector<Player>
{
	public override IEnumerable<Player> Evaluate(Context context)
	{
		var players = playerSelector?.Evaluate(context) ?? [];
		var tags = tagSelector?.Evaluate(context) ?? [];
		
		return players.Where(player => tags.All(tag => player.GetQueriedTag(tag, out _)));

	}
}