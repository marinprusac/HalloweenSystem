using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.RuleActions;

/// <summary>
/// Represents an abstract base class for rule actions.
/// </summary>
public abstract class RuleAction
{
    /// <summary>
    /// Evaluates the action in the given context.
    /// </summary>
    /// <param name="context">The context in which to evaluate the action.</param>
    public abstract void Evaluate(Context context);
}