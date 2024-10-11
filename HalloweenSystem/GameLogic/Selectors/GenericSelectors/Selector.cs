using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors;

/// <summary>
/// A selector is a class that evaluates a context and returns a list of objects.
/// This class serves as a base class for all selectors.
/// </summary>
/// <typeparam name="T">type of the objects returned in a list after evaluation</typeparam>
public abstract class Selector<T> : IEvaluator<IEnumerable<T>> where T : GameObject
{
	/// <summary>
	/// Evaluates the context and returns a collection of game objects.
	/// </summary>
	/// <param name="context">The context in which to evaluate the selector.</param>
	/// <returns>An enumerable collection of game objects.</returns>
	public abstract IEnumerable<T> Evaluate(Context context);
}