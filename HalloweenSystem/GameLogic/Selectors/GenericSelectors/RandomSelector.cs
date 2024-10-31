using System.Xml;
using HalloweenSystem.GameLogic.Parsing;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors;

/// <summary>
/// A selector that randomly selects a specified amount of game objects from a nested selector.
/// </summary>
/// <typeparam name="T">The type of game object to be selected.</typeparam>
/// <param name="amount">The amount of game objects to select.</param>
/// <param name="nestedSelector">An optional nested selector to use for selecting game objects. If not provided, an AllSelector is used.</param>
public class RandomSelector<T>(string amount, ISelector<T>? nestedSelector=null) : ISelector<T>, IParser<RandomSelector<T>> where T : GameObject, new()
{
	private ISelector<T>? _nestedSelector = nestedSelector;

	/// <summary>
	/// Evaluates the context and returns a random selection of game objects.
	/// </summary>
	/// <param name="context">The context in which to evaluate the selector.</param>
	/// <returns>An enumerable collection of randomly selected game objects.</returns>
	public IEnumerable<T> Evaluate(Context context)
	{
		_nestedSelector ??= new AllSelector<T>();
		var gameObjects = _nestedSelector.Evaluate(context).ToList();
		var selectedCount = Limit.GetCount(amount, gameObjects.Count);
		
		if(selectedCount >= gameObjects.Count) return gameObjects;
		
		
		return Limit.ChooseRandom(gameObjects, selectedCount);
	}

	public static RandomSelector<T> Parse(XmlNode node)
	{
		if(node.Attributes?["amount"] == null) throw new XmlException("Expected 'amount' attribute.");
		var amount = node.Attributes["amount"]!.Value;
		var nestedSelector = ListSelector<T>.Parse(node);
		return new RandomSelector<T>(amount, nestedSelector);
	}
}