using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.TagSelectors;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.Selectors.TagSelectors;

public class ExtractTagSelector(TagType tagType, TagSelector nestedTagSelector) : TagSelector
{
	public override IEnumerable<AssignedTag> Evaluate(Context context, Player? operatedPlayer = null)
	{
		var tags = nestedTagSelector.Evaluate(context, operatedPlayer);
		var extractedTags = tags.SelectMany(tag => tag.TagTypeParameters).ToHashSet();
		return extractedTags.Select(tag => new AssignedTag(tag));
	}
}