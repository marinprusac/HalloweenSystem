using System.Linq;
using System.Xml;
using HalloweenSystem.GameLogic.GameObjects;
using HalloweenSystem.GameLogic.Parsing;
using HalloweenSystem.GameLogic.Selectors;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.RuleActions;

/// <summary>
/// Represents an action that activates rules based on a specified rule selector.
/// </summary>
/// <param name="ruleSelector">The selector that evaluates to a collection of rules to be activated.</param>
public class ActivateAction(ISelector<Rule> ruleSelector) : IAction, IParser<ActivateAction>
{
    /// <summary>
    /// Evaluates the action in the given context by activating the rules selected by the rule selector.
    /// </summary>
    /// <param name="context">The context in which to evaluate the action.</param>
    public void Evaluate(Context context)
    {
        ruleSelector.Evaluate(context).ToList().ForEach(rule => rule.Evaluate(context));
    }

    public static ActivateAction Parse(XmlNode node)
    {
        if(!node.HasChildNodes) throw new XmlException("Expected a rule selector child node.");
        var ruleSelector = Parser.ParseSelector<Rule>(node.FirstChild!);
        return new ActivateAction(ruleSelector);
    }
}