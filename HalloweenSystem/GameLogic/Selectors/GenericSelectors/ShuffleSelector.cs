using System.Xml;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors
{
	/// <summary>
	/// A selector that selects all game objects of a specified type.
	/// </summary>
	/// <typeparam name="T">The type of game object to be selected. Must inherit from GameObject and have a parameterless constructor.</typeparam>
	public class ShuffleSelector<T>(ISelector<T> objectSelector) : ISelector<T>, IParser<ShuffleSelector<T>> where T : GameObject, new()
	{
		/// <summary>
		/// Evaluates the context and returns a collection of all game objects of the specified type.
		/// </summary>
		/// <param name="context">The context in which to evaluate the selector.</param>
		/// <returns>An enumerable collection of all game objects of the specified type.</returns>
		public IEnumerable<T> Evaluate(Context context)
		{
			var objectList = objectSelector.Evaluate(context).ToArray();
			var random = new Random();
			random.Shuffle(objectList);
			return objectList;
		}

		public static ShuffleSelector<T> Parse(XmlNode node)
		{
			var list = ListSelector<T>.Parse(node);
			return new ShuffleSelector<T>(list);
		}
	}
}