using System.Linq;
using HalloweenSystem.GameLogic.Selectors;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.RuleActions;

/// <summary>
/// Represents an action that activates rules based on a specified rule selector.
/// </summary>
/// <param name="ruleSelector">The selector that evaluates to a collection of rules to be activated.</param>
public class ActivateAction(Selector<Rule> ruleSelector) : RuleAction
{
    /// <summary>
    /// Evaluates the action in the given context by activating the rules selected by the rule selector.
    /// </summary>
    /// <param name="context">The context in which to evaluate the action.</param>
    public override void Evaluate(Context context)
    {
        ruleSelector.Evaluate(context).ToList().ForEach(rule => rule.Evaluate(context));
    }
}