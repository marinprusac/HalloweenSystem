using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.TagSelectors;

public class GroupTagSelector(TagGroup tagGroup, TagSelector nestedTagSelector) : TagSelector
{
	public override IEnumerable<AssignedTag> Evaluate(Context context, Player? operatedPlayer = null)
	{
		var tags = nestedTagSelector.Evaluate(context, operatedPlayer);
		var groupTags = tags.Where(t => tagGroup.Tags.Contains(t.Type));
		return groupTags;
	}
}