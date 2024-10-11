using System.Collections.Generic;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.RuleSelectors;

/// <summary>
/// Represents a selector that selects a specific rule.
/// </summary>
/// <param name="rule">The rule to be selected.</param>
public class RuleSelector(Rule rule) : Selector<Rule>
{
    /// <summary>
    /// Evaluates the selector in the given context and returns a collection containing the specified rule.
    /// </summary>
    /// <param name="context">The context in which to evaluate the selector.</param>
    /// <returns>A collection containing the specified rule.</returns>
    public override IEnumerable<Rule> Evaluate(Context context)
    {
        return [rule];
    }
}