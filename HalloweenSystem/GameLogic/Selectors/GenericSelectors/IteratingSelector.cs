using System.Xml;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors;

/// <summary>
/// A selector that iterates over a collection of game objects, using a parameter selector to set a context parameter for each iteration.
/// </summary>
/// <typeparam name="TP">The type of the parameter selector. Must inherit from GameObject.</typeparam>
/// <typeparam name="TI">The type of the iterable selector. Must inherit from GameObject.</typeparam>
/// <param name="parameterName">The name of the parameter to set in the context for each iteration.</param>
/// <param name="parameterSelector">The selector used to obtain the parameters for iteration.</param>
/// <param name="iteratingSelector">The selector used to evaluate the context for each parameter.</param>
public class IteratingSelector<TP, TI>(
	string parameterName,
	ISelector<TP> parameterSelector,
	ISelector<TI> iteratingSelector) : ISelector<TI>, IParser<IteratingSelector<TP, TI>> where TI : GameObject where TP : GameObject
{
	/// <summary>
	/// Evaluates the context and returns a collection of game objects by iterating over the parameters and applying the iterating selector.
	/// </summary>
	/// <param name="context">The context in which to evaluate the selector.</param>
	/// <returns>An enumerable collection of game objects resulting from the iteration.</returns>
	public IEnumerable<TI> Evaluate(Context context)
	{
		var parameters = parameterSelector.Evaluate(context).ToList();
		var result = new List<TI>();

		foreach (var parameter in parameters)
		{
			context.Parameters[parameterName] = parameter;
			var evaluation = iteratingSelector.Evaluate(context);
			result.AddRange(evaluation);
		}

		return result;
	}

	public static IteratingSelector<TP, TI> Parse(XmlNode node)
	{
		throw new NotImplementedException();
	}
}