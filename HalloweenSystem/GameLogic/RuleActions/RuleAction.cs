using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.RuleActions;

/// <summary>
/// Represents an abstract base class for rule actions.
/// </summary>
public interface IAction
{
    /// <summary>
    /// Evaluates the action in the given context.
    /// </summary>
    /// <param name="context">The context in which to evaluate the action.</param>
    public void Evaluate(Context context);
}