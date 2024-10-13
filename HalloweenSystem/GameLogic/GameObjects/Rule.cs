using System.Xml;
using HalloweenSystem.GameLogic.Parsing;
using HalloweenSystem.GameLogic.RuleActions;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.GameObjects;

/// <summary>
/// Represents a rule in the game logic, which consists of a name, a requirement selector, and a list of actions.
/// </summary>
/// <param name="name">The name of the rule.</param>
/// <param name="actions">The list of actions to be executed when the rule is evaluated.</param>
public class Rule(string name, List<IAction> actions) : GameObject(name), IParser<Rule>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Rule"/> class with a name, a requirement selector, and a list of actions.
    /// </summary>
    /// <param name="name">The name of the rule.</param>
    /// <param name="requirement">The selector that determines whether the rule's actions should be executed.</param>
    /// <param name="actions">The list of actions to be executed when the rule is evaluated.</param>
    public Rule(string name, ISelector<Player> requirement, List<IAction> actions) : this(name, actions)
    {
        _requirement = requirement;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rule"/> class with a default name and an empty list of actions.
    /// </summary>
    public Rule() : this("", [])
    {
    }

    /// <summary>
    /// The selector that determines whether the rule's actions should be executed.
    /// </summary>
    private readonly ISelector<Player> _requirement = new AllSelector<Player>();

    /// <summary>
    /// Evaluates the rule in the given context. If the requirement selector returns any players, the rule's actions are executed.
    /// </summary>
    /// <param name="context">The context in which to evaluate the rule.</param>
    public void Evaluate(Context context)
    {
        if (!_requirement.Evaluate(context).Any()) return;
        actions.ForEach(action => action.Evaluate(context));
    }

    /// <summary>
    /// Retrieves all game objects associated with the rule in the given context.
    /// </summary>
    /// <param name="context">The context in which to retrieve the game objects.</param>
    /// <returns>A collection of game objects associated with the rule.</returns>
    protected override IEnumerable<GameObject> _everything(Context context)
    {
        return context.Setting.Rules;
    }

    /// <summary>
    /// Retrieves the complement of the given set of game objects in the context.
    /// </summary>
    /// <param name="objectSet">The set of game objects to complement.</param>
    /// <param name="context">The context in which to retrieve the complement.</param>
    /// <returns>A collection of game objects that are not in the given set.</returns>
    protected override IEnumerable<GameObject> _complement(IEnumerable<GameObject> objectSet, Context context)
    {
        return context.Setting.Rules.Except(objectSet);
    }

    public static Rule Parse(XmlNode node)
    {
        if (node.Attributes?["name"] == null) throw new XmlException("Expected 'name' attribute.");
        var name = node.Attributes["name"]!.Value;

        var requirementNode = node.SelectSingleNode("requirement/*");
        var actionNodes = node.SelectNodes("actions/*");

        var requirement = requirementNode != null
            ? Parser.ParseSelector<Player>(requirementNode)
            : new AllSelector<Player>();

        var actions = actionNodes != null
            ? actionNodes.Cast<XmlNode>().Select(Parser.ParseAction).ToList()
            : [];

        return new Rule(name, requirement, actions);
    }
}