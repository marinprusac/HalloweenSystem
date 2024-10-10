using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.TagSelectors;

public class TypeFilterTagSelector(string tagType, Selector<Tag> tagSelector) : Selector<Tag>
{
	public override IEnumerable<Tag> Evaluate(Context context)
	{
		var tags = tagSelector.Evaluate(context);
		return tags.Where(tag => tag.Name == tagType);
	}
}