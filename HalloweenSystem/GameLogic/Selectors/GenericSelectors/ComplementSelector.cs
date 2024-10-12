using System.Xml;
using HalloweenSystem.GameLogic.Parsing;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors
{
	/// <summary>
	/// A selector that selects the complement of game objects from a nested selector.
	/// </summary>
	/// <typeparam name="T">The type of game object to be selected. Must inherit from GameObject and have a parameterless constructor.</typeparam>
	/// <param name="nestedSelector">The nested selector to use for selecting game objects.</param>
	public class ComplementSelector<T>(ISelector<T> nestedSelector) : ISelector<T>, IParser<ComplementSelector<T>> where T : GameObject, new()
	{
		/// <summary>
		/// Evaluates the context and returns a collection of game objects that are the complement of those selected by the nested selector.
		/// </summary>
		/// <param name="context">The context in which to evaluate the selector.</param>
		/// <returns>An enumerable collection of game objects that are the complement of those selected by the nested selector.</returns>
		public IEnumerable<T> Evaluate(Context context)
		{
			var gameObjects = nestedSelector.Evaluate(context);
			return GameObject.Complement<T>(gameObjects, context).Cast<T>();
		}

		public static ComplementSelector<T> Parse(XmlNode node)
		{
			if(node.Name != "complement") throw new XmlException("Expected <complement> node.");
			if(node.ChildNodes.Count != 1) throw new XmlException("Expected exactly one child node.");
			var child = node.ChildNodes[0]!;
			var nestedSelector = Parser.ParseSelector<T>(child);
			return new ComplementSelector<T>(nestedSelector);
		}
	}
}