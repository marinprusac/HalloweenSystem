using System.Collections.Generic;
using System.Linq;
using System.Xml;
using HalloweenSystem.GameLogic.GameObjects;
using HalloweenSystem.GameLogic.Parsing;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.TagSelectors;

/// <summary>
/// Represents a selector that filters tags based on a specified tag type.
/// </summary>
/// <param name="tagType">The type of tag to filter.</param>
/// <param name="tagSelector">The selector that evaluates to a collection of tags.</param>
public class TypeFilterTagSelector(string tagType, ISelector<Tag> tagSelector) : ISelector<Tag>, IParser<TypeFilterTagSelector>
{
    /// <summary>
    /// Evaluates the selector in the given context and returns a collection of tags that match the specified tag type.
    /// </summary>
    /// <param name="context">The context in which to evaluate the selector.</param>
    /// <returns>A collection of tags that match the specified tag type.</returns>
    public IEnumerable<Tag> Evaluate(Context context)
    {
        var tags = tagSelector.Evaluate(context);
        return tags.Where(tag => tag.Name == tagType);
    }

    public static TypeFilterTagSelector Parse(XmlNode node)
    {
        if (node.Attributes?["type"] == null) throw new XmlException("Expected 'type' attribute.");
        var tagType = node.Attributes["type"]!.Value;

        var tagSelectorNode = node.FirstChild;
        var tagSelector = tagSelectorNode == null
            ? throw new XmlException("Expected a tag selector.")
            : Parser.ParseSelector<Tag>(tagSelectorNode);

        return new TypeFilterTagSelector(tagType, tagSelector);
    }
}