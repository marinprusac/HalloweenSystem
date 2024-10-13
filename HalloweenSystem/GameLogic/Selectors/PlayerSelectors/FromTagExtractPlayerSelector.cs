using System.Collections.Generic;
using System.Linq;
using System.Xml;
using HalloweenSystem.GameLogic.GameObjects;
using HalloweenSystem.GameLogic.Parsing;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.PlayerSelectors;

/// <summary>
/// Represents a selector that extracts players from tags using a nested selector.
/// </summary>
/// <param name="nestedSelector">The nested selector that evaluates to a collection of tags.</param>
public class FromTagExtractPlayerSelector(ISelector<Tag> nestedSelector)
	: ISelector<Player>, IParser<FromTagExtractPlayerSelector>
{
	/// <summary>
	/// Evaluates the selector in the given context and returns a collection of players extracted from the tags.
	/// </summary>
	/// <param name="context">The context in which to evaluate the selector.</param>
	/// <returns>A collection of players extracted from the tags.</returns>
	public IEnumerable<Player> Evaluate(Context context)
	{
		var tags = nestedSelector.Evaluate(context);

		var playerParameters = tags.Select(tag => tag.PlayerParameters.AsEnumerable());

		return GameObject.Union<Player>(playerParameters).Cast<Player>();
	}

	public static FromTagExtractPlayerSelector Parse(XmlNode node)
	{
		if (node.HasChildNodes == false) throw new XmlException("Expected a nested selector.");
		var nestedSelector = Parser.ParseSelector<Tag>(node.FirstChild!);
		return new FromTagExtractPlayerSelector(nestedSelector);
	}
}