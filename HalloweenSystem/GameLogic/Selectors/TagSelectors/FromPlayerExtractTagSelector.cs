using System.Collections.Generic;
using System.Linq;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.TagSelectors;

public class FromPlayerExtractTagSelector(string tagType, Selector<Player> playerSelector) : Selector<Tag>
{
	public override IEnumerable<Tag> Evaluate(Context context)
	{
		var players = playerSelector.Evaluate(context);
		var tags = players.Select(p => p.AssignedTags.AsEnumerable());
		var unionizedTags = GameObject.Union<Tag>(tags);
		var filtered = unionizedTags.Where(t => t.Name == tagType);
		return filtered.Cast<Tag>();
	}
}