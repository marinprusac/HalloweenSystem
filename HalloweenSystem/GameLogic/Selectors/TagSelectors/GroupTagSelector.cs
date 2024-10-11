using System.Collections.Generic;
using System.Linq;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.TagSelectors;

/// <summary>
/// Represents a selector that groups tags based on a specified tag group name.
/// </summary>
/// <param name="tagGroupName">The name of the tag group to match.</param>
/// <param name="tagSelector">The optional selector that evaluates to a collection of tags. If not provided, all tags are considered.</param>
public class GroupTagSelector(string tagGroupName, Selector<Tag>? tagSelector=null) : Selector<Tag>
{
    private Selector<Tag>? _tagSelector = tagSelector;

    /// <summary>
    /// Evaluates the selector in the given context and returns a collection of tags that belong to the specified tag group.
    /// </summary>
    /// <param name="context">The context in which to evaluate the selector.</param>
    /// <returns>A collection of tags that belong to the specified tag group.</returns>
    public override IEnumerable<Tag> Evaluate(Context context)
    {
        _tagSelector ??= new AllSelector<Tag>();
        var tags = _tagSelector.Evaluate(context);
        var tagGroup = context.Setting.TagGroups.FirstOrDefault(tg => tg.Name == tagGroupName);
        if (tagGroup == null) return Enumerable.Empty<Tag>();
        var groupTags = tags.Where(t => tagGroup.Tags.Contains(t.Name));
        return groupTags;
    }
}