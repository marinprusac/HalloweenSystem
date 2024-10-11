using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors
{
	/// <summary>
	/// A selector that selects game objects based on a specified parameter.
	/// </summary>
	/// <typeparam name="T">The type of game object to be selected. Must inherit from GameObject.</typeparam>
	/// <param name="parameterName">The name of the parameter to use for selection.</param>
	public class ParameterSelector<T>(string parameterName) : Selector<T> where T : GameObject
	{
		/// <summary>
		/// Evaluates the context and returns a collection of game objects based on the specified parameter.
		/// </summary>
		/// <param name="context">The context in which to evaluate the selector.</param>
		/// <returns>An enumerable collection of game objects based on the specified parameter.</returns>
		public override IEnumerable<T> Evaluate(Context context)
		{
			var value = context.Parameters[parameterName];
			var output = new List<T>([(T)value]);
			return output;
		}
	}
}