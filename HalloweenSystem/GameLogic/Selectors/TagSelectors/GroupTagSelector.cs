using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.TagSelectors;

public class GroupTagSelector(string tagGroupName, Selector<Tag>? tagSelector=null) : Selector<Tag>
{
	private Selector<Tag>? _tagSelector = tagSelector;

	public override IEnumerable<Tag> Evaluate(Context context)
	{
		_tagSelector ??= new EverySelector<Tag>();
		var tags = _tagSelector.Evaluate(context);
		var tagGroup = context.Setting.TagGroups.FirstOrDefault(tg => tg.Name == tagGroupName);
		if (tagGroup == null) return Enumerable.Empty<Tag>();
		var groupTags = tags.Where(t => tagGroup.Tags.Contains(t.Name));
		return groupTags;
	}
}