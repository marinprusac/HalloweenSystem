using System.Xml;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors
{
	/// <summary>
	/// A selector that always returns an empty collection of game objects.
	/// </summary>
	/// <typeparam name="T">The type of game object to be selected. Must inherit from GameObject.</typeparam>
	public class EmptySelector<T> : ISelector<T>, IParser<EmptySelector<T>> where T : GameObject
	{
		/// <summary>
		/// Evaluates the context and returns an empty collection of game objects.
		/// </summary>
		/// <param name="context">The context in which to evaluate the selector.</param>
		/// <returns>An empty enumerable collection of game objects.</returns>
		public IEnumerable<T> Evaluate(Context context)
		{
			return [];
		}

		public static EmptySelector<T> Parse(XmlNode node)
		{
			return new EmptySelector<T>();
		}
	}
}