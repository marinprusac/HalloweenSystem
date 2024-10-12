using System.Collections.Generic;
using System.Linq;
using System.Xml;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.TagSelectors;

/// <summary>
/// Represents a selector that evaluates to a collection of tags used by players in the context.
/// </summary>
public class UsedTagSelector : ISelector<Tag>, IParser<UsedTagSelector>
{
    /// <summary>
    /// Evaluates the selector in the given context and returns a collection of tags used by players.
    /// </summary>
    /// <param name="context">The context in which to evaluate the selector.</param>
    /// <returns>A collection of tags used by players.</returns>
    public IEnumerable<Tag> Evaluate(Context context)
    {
        var tags = context.Players.Select(p => p.AssignedTags.AsEnumerable());
        var tagUnion = GameObject.Union<Tag>(tags).Cast<Tag>();
        return tagUnion;
    }

    public static UsedTagSelector Parse(XmlNode node)
    {
        throw new NotImplementedException();
    }
}