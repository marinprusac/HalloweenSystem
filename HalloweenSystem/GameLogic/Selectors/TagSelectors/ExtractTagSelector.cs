using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.Selectors.TagSelectors;

public class ExtractTagSelector(Selector<Tag> nestedTagSelector) : Selector<Tag>
{
	public override IEnumerable<Tag> Evaluate(Context context)
	{
		var tags = nestedTagSelector.Evaluate(context);
		var extractedTags = tags.SelectMany(tag => tag.TagTypeParameters).ToHashSet();
		return extractedTags.Select(tag => new Tag(tag));
	}
}