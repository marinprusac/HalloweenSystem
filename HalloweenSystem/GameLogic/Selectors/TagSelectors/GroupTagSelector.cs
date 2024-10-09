using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.Selectors.TagSelectors;

public class GroupTagSelector(TagGroup tagGroup, Selector<Tag> nestedTagSelector) : Selector<Tag>
{
	public override IEnumerable<Tag> Evaluate(Context context)
	{
		var tags = nestedTagSelector.Evaluate(context);
		var groupTags = tags.Where(t => tagGroup.Tags.Contains(t.Name));
		return groupTags;
	}
}