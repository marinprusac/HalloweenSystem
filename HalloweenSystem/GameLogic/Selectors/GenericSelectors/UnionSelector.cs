using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors;

/// <summary>
/// A selector that combines the results of multiple nested selectors into a single collection of game objects.
/// </summary>
/// <typeparam name="T">The type of game object to be selected. Must inherit from GameObject.</typeparam>
/// <param name="nestedSelectors">The collection of nested selectors to combine.</param>
public class UnionSelector<T>(IEnumerable<Selector<T>> nestedSelectors) : Selector<T> where T : GameObject, new()
{
	/// <summary>
	/// Evaluates the context and returns a combined collection of game objects from all nested selectors.
	/// </summary>
	/// <param name="context">The context in which to evaluate the selectors.</param>
	/// <returns>An enumerable collection of game objects combined from all nested selectors.</returns>
	public override IEnumerable<T> Evaluate(Context context)
	{
		var evaluations = nestedSelectors.Select(selector => selector.Evaluate(context)).ToList();
		var result = GameObject.Union<T>(evaluations);
		return result.Cast<T>();
	}
}