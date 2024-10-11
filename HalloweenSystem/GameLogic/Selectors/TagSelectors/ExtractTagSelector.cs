using System.Collections.Generic;
using System.Linq;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.TagSelectors;

public class ExtractTagSelector(Selector<Tag> tagSelector) : Selector<Tag>
{
	public override IEnumerable<Tag> Evaluate(Context context)
	{
		var tags = tagSelector.Evaluate(context);
		var extractedTags = tags.SelectMany(tag => tag.TagTypeParameters).ToHashSet();
		return extractedTags.Select(tag => new Tag(tag));
	}
}