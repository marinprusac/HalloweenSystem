using System.Collections.Generic;
using System.Xml;
using HalloweenSystem.GameLogic.GameObjects;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.RuleSelectors;

/// <summary>
/// Represents a selector that selects a specific rule.
/// </summary>
/// <param name="rule">The rule to be selected.</param>
public class RuleSelector(string ruleName) : ISelector<Rule>, IParser<RuleSelector>
{
    /// <summary>
    /// Evaluates the selector in the given context and returns a collection containing the specified rule.
    /// </summary>
    /// <param name="context">The context in which to evaluate the selector.</param>
    /// <returns>A collection containing the specified rule.</returns>
    public IEnumerable<Rule> Evaluate(Context context)
    {
        var rule = context.Setting.Rules.FirstOrDefault(r => r.Name == ruleName);
        if (rule == null) throw new ArgumentException("Rule not found.");
        return [rule];
    }

    public static RuleSelector Parse(XmlNode node)
    {
        var ruleName = node.Attributes?["name"]?.Value;
        if (ruleName == null) throw new XmlException("Expected 'name' attribute.");
        return new RuleSelector(ruleName);
    }
}