using System.Collections.Generic;
using System.Linq;
using System.Xml;
using HalloweenSystem.GameLogic.GameObjects;
using HalloweenSystem.GameLogic.Parsing;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.TagSelectors;

/// <summary>
/// Represents a selector that extracts tags from other tags based on their type parameters.
/// </summary>
/// <param name="tagSelector">The selector that evaluates to a collection of tags.</param>
public class FromTagExtractTagSelector(ISelector<Tag> tagSelector)
	: ISelector<Tag>, IParser<FromTagExtractTagSelector>
{
	/// <summary>
	/// Evaluates the selector in the given context and returns a collection of tags extracted from the evaluated tags.
	/// </summary>
	/// <param name="context">The context in which to evaluate the selector.</param>
	/// <returns>A collection of tags extracted from the evaluated tags.</returns>
	public IEnumerable<Tag> Evaluate(Context context)
	{
		var tags = tagSelector.Evaluate(context);
		var extractedTags = tags.SelectMany(tag => tag.tagTypeParameters).ToHashSet();
		return extractedTags.Select(tag => new Tag(tag));
	}

	public static FromTagExtractTagSelector Parse(XmlNode node)
	{
		var tagSelector = ListSelector<Tag>.Parse(node);
		return new FromTagExtractTagSelector(tagSelector);
	}
}