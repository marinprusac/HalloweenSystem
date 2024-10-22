using System.Xml;
using HalloweenSystem.GameLogic.Parsing;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors
{
	/// <summary>
	/// A selector that selects game objects that are common to all nested selectors.
	/// </summary>
	/// <typeparam name="T">The type of game object to be selected. Must inherit from GameObject and have a parameterless constructor.</typeparam>
	/// <param name="nestedSelectors">The collection of nested selectors to use for selecting game objects.</param>
	public class IntersectSelector<T>(IEnumerable<ISelector<T>> nestedSelectors)
		: ISelector<T>, IParser<IntersectSelector<T>> where T : GameObject, new()
	{
		/// <summary>
		/// Evaluates the context and returns a collection of game objects that are common to all nested selectors.
		/// </summary>
		/// <param name="context">The context in which to evaluate the selector.</param>
		/// <returns>An enumerable collection of game objects that are common to all nested selectors.</returns>
		public IEnumerable<T> Evaluate(Context context)
		{
			var evaluations = nestedSelectors.Select(selector => selector.Evaluate(context)).ToList();
			var result = GameObject.Intersect<T>(evaluations);
			return result.Cast<T>();
		}

		public static IntersectSelector<T> Parse(XmlNode node)
		{
			var selectors = (from XmlNode child in node.ChildNodes select Parser.ParseSelector<T>(child)).ToList();
			return new IntersectSelector<T>(selectors);
		}
	}
}