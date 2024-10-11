using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors;

/// <summary>
/// A selector that randomly selects a specified amount of game objects from a nested selector.
/// </summary>
/// <typeparam name="T">The type of game object to be selected.</typeparam>
/// <param name="amount">The amount of game objects to select.</param>
/// <param name="nestedSelector">An optional nested selector to use for selecting game objects. If not provided, an AllSelector is used.</param>
public class RandomSelector<T>(string amount, Selector<T>? nestedSelector=null) : Selector<T> where T : GameObject, new()
{
	private Selector<T>? _nestedSelector = nestedSelector;

	/// <summary>
	/// Evaluates the context and returns a random selection of game objects.
	/// </summary>
	/// <param name="context">The context in which to evaluate the selector.</param>
	/// <returns>An enumerable collection of randomly selected game objects.</returns>
	public override IEnumerable<T> Evaluate(Context context)
	{
		_nestedSelector ??= new AllSelector<T>();
		var gameObjects = _nestedSelector.Evaluate(context);
		var limit = new Limit<T>(amount);
		return limit.Evaluate(gameObjects);
	}
}