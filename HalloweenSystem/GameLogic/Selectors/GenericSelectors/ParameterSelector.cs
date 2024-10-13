using System.Xml;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors
{
	/// <summary>
	/// A selector that selects game objects based on a specified parameter.
	/// </summary>
	/// <typeparam name="T">The type of game object to be selected. Must inherit from GameObject.</typeparam>
	/// <param name="parameterName">The name of the parameter to use for selection.</param>
	public class ParameterSelector<T>(string parameterName) : ISelector<T>, IParser<ParameterSelector<T>> where T : GameObject
	{
		/// <summary>
		/// Evaluates the context and returns a collection of game objects based on the specified parameter.
		/// </summary>
		/// <param name="context">The context in which to evaluate the selector.</param>
		/// <returns>An enumerable collection of game objects based on the specified parameter.</returns>
		public IEnumerable<T> Evaluate(Context context)
		{
			var value = context.Parameters[parameterName];
			var output = new List<T>([(T)value]);
			return output;
		}

		public static ParameterSelector<T> Parse(XmlNode node)
		{
			if (node.Attributes?["name"] == null) throw new XmlException("Expected 'name' attribute.");
			var parameterName = node.Attributes["name"]!.Value;
			return new ParameterSelector<T>(parameterName);
		}
	}
}