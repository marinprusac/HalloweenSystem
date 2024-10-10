using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.TagSelectors;

public class UsedTagSelector : Selector<Tag>
{
	public override IEnumerable<Tag> Evaluate(Context context)
	{
		var tags = context.Players.Select(p => p.AssignedTags.AsEnumerable());
		var tagUnion = GameObject.Union<Tag>(tags).Cast<Tag>();
		return tagUnion;
	}
}