using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors;

/// <summary>
/// A selector that combines multiple selectors into a single list of game objects.
/// </summary>
/// <typeparam name="T">The type of game object to be selected. Must inherit from GameObject.</typeparam>
/// <param name="selectorList">The list of selectors to combine.</param>
public class ListSelector<T>(params Selector<T>[] selectorList) : Selector<T> where T : GameObject
{
	/// <summary>
	/// Evaluates the context and returns a combined collection of game objects from all selectors in the list.
	/// </summary>
	/// <param name="context">The context in which to evaluate the selectors.</param>
	/// <returns>An enumerable collection of game objects combined from all selectors in the list.</returns>
	public override IEnumerable<T> Evaluate(Context context)
	{
		var results = selectorList.SelectMany(selector => selector.Evaluate(context).ToList()).ToList();
		return results;
	}
}