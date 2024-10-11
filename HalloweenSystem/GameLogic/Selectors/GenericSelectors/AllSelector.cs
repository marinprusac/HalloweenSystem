using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors
{
	/// <summary>
	/// A selector that selects all game objects of a specified type.
	/// </summary>
	/// <typeparam name="T">The type of game object to be selected. Must inherit from GameObject and have a parameterless constructor.</typeparam>
	public class AllSelector<T> : Selector<T> where T : GameObject, new()
	{
		/// <summary>
		/// Evaluates the context and returns a collection of all game objects of the specified type.
		/// </summary>
		/// <param name="context">The context in which to evaluate the selector.</param>
		/// <returns>An enumerable collection of all game objects of the specified type.</returns>
		public override IEnumerable<T> Evaluate(Context context)
		{
			return GameObject.Everything<T>(context).Cast<T>();
		}
	}
}