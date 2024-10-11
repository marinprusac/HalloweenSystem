using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors
{
	/// <summary>
	/// A selector that selects game objects based on a specified probability.
	/// </summary>
	/// <typeparam name="T">The type of game object to be selected. Must inherit from GameObject.</typeparam>
	/// <param name="probability">The probability of selecting each game object.</param>
	/// <param name="nestedSelector">The nested selector to use for selecting game objects.</param>
	public class ChanceSelector<T>(float probability, Selector<T> nestedSelector) : Selector<T> where T : GameObject
	{
		/// <summary>
		/// Evaluates the context and returns a collection of game objects based on the specified probability.
		/// </summary>
		/// <param name="context">The context in which to evaluate the selector.</param>
		/// <returns>An enumerable collection of game objects selected based on the specified probability.</returns>
		public override IEnumerable<T> Evaluate(Context context)
		{
			var objects = nestedSelector.Evaluate(context);
			var random = new Random();
			var chosen = objects.Where(gameObject => random.NextSingle() < probability).ToList();
			return chosen;
		}
	}
}