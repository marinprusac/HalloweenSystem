using System.Collections.Generic;
using System.Linq;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.TagSelectors;

/// <summary>
/// Represents a selector that filters tags based on a specified tag type.
/// </summary>
/// <param name="tagType">The type of tag to filter.</param>
/// <param name="tagSelector">The selector that evaluates to a collection of tags.</param>
public class TypeFilterTagSelector(string tagType, Selector<Tag> tagSelector) : Selector<Tag>
{
    /// <summary>
    /// Evaluates the selector in the given context and returns a collection of tags that match the specified tag type.
    /// </summary>
    /// <param name="context">The context in which to evaluate the selector.</param>
    /// <returns>A collection of tags that match the specified tag type.</returns>
    public override IEnumerable<Tag> Evaluate(Context context)
    {
        var tags = tagSelector.Evaluate(context);
        return tags.Where(tag => tag.Name == tagType);
    }
}