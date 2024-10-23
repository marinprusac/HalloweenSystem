using System.Xml;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors;


	
public class AllIfAnySelector<T>(ISelector<T> nestedSelector) : ISelector<T>, IParser<AllIfAnySelector<T>> where T : GameObject, new()
{
	/// <summary>
	/// Evaluates the context and returns a collection of game objects that are the complement of those selected by the nested selector.
	/// </summary>
	/// <param name="context">The context in which to evaluate the selector.</param>
	/// <returns>An enumerable collection of game objects that are the complement of those selected by the nested selector.</returns>
	public IEnumerable<T> Evaluate(Context context)
	{
		var gameObjects = nestedSelector.Evaluate(context);
		return gameObjects.Any() ? GameObject.Everything<T>(context).Cast<T>() : [];
	}

	public static AllIfAnySelector<T> Parse(XmlNode node)
	{
		var nestedSelector = ListSelector<T>.Parse(node);
		return new AllIfAnySelector<T>(nestedSelector);
	}
}
