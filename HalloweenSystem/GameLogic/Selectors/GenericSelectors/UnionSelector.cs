using System.Xml;
using HalloweenSystem.GameLogic.Parsing;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors;

/// <summary>
/// A selector that combines the results of multiple nested selectors into a single collection of game objects.
/// </summary>
/// <typeparam name="T">The type of game object to be selected. Must inherit from GameObject.</typeparam>
/// <param name="nestedSelectors">The collection of nested selectors to combine.</param>
public class UnionSelector<T>(IEnumerable<ISelector<T>> nestedSelectors) : ISelector<T>, IParser<UnionSelector<T>> where T : GameObject, new()
{
	/// <summary>
	/// Evaluates the context and returns a combined collection of game objects from all nested selectors.
	/// </summary>
	/// <param name="context">The context in which to evaluate the selectors.</param>
	/// <returns>An enumerable collection of game objects combined from all nested selectors.</returns>
	public IEnumerable<T> Evaluate(Context context)
	{
		var evaluations = nestedSelectors.Select(selector => selector.Evaluate(context)).ToList();
		var result = GameObject.Union<T>(evaluations);
		return result.Cast<T>();
	}

	public static UnionSelector<T> Parse(XmlNode node)
	{
		if (node.ChildNodes.Count == 0) throw new XmlException("Expected at least 1 child node.");
		var selectors = (from XmlNode child in node.ChildNodes select Parser.ParseSelector<T>(child)).ToList();
		return new UnionSelector<T>(selectors);
	}
}