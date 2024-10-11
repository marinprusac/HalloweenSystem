using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Utilities;

/// <summary>
/// Represents an evaluator that can evaluate a context and return a result of type T.
/// </summary>
/// <typeparam name="T">The type of the result returned by the evaluator.</typeparam>
public interface IEvaluator<out T>
{
    /// <summary>
    /// Evaluates the given context and returns a result of type T.
    /// </summary>
    /// <param name="context">The context to evaluate.</param>
    /// <returns>The result of the evaluation.</returns>
    T Evaluate(Context context);
}