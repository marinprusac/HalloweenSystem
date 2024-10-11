using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors
{
	/// <summary>
	/// A selector that always returns an empty collection of game objects.
	/// </summary>
	/// <typeparam name="T">The type of game object to be selected. Must inherit from GameObject.</typeparam>
	public class EmptySelector<T> : Selector<T> where T : GameObject
	{
		/// <summary>
		/// Evaluates the context and returns an empty collection of game objects.
		/// </summary>
		/// <param name="context">The context in which to evaluate the selector.</param>
		/// <returns>An empty enumerable collection of game objects.</returns>
		public override IEnumerable<T> Evaluate(Context context)
		{
			return [];
		}
	}
}