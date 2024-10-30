using System.Xml;
using HalloweenSystem.GameLogic.Parsing;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors
{
	/// <summary>
	/// A selector that selects game objects based on a specified probability.
	/// </summary>
	/// <typeparam name="T">The type of game object to be selected. Must inherit from GameObject.</typeparam>
	/// <param name="probability">The probability of selecting each game object.</param>
	/// <param name="nestedSelector">The nested selector to use for selecting game objects.</param>
	public class ChanceSelector<T>(float probability, ISelector<T> nestedSelector) : ISelector<T>, IParser<ChanceSelector<T>> where T : GameObject, new()
	{
		/// <summary>
		/// Evaluates the context and returns a collection of game objects based on the specified probability.
		/// </summary>
		/// <param name="context">The context in which to evaluate the selector.</param>
		/// <returns>An enumerable collection of game objects selected based on the specified probability.</returns>
		public IEnumerable<T> Evaluate(Context context)
		{
			var objects = nestedSelector.Evaluate(context).ToList();
			var random = new Random();
			context.Setting.Statistics.LogCombination(2^objects.Distinct().Count());
			var chosen = objects.Where(gameObject => random.NextSingle() < probability).ToList();
			return chosen;
		}

		public static ChanceSelector<T> Parse(XmlNode node) 
		{
			if (node.Attributes?["probability"] == null) throw new XmlException("Expected 'probability' attribute.");
			var probability = float.Parse(node.Attributes["probability"]!.Value);
			var nestedSelector = ListSelector<T>.Parse(node);
			return new ChanceSelector<T>(probability, nestedSelector);
		}
	}
}