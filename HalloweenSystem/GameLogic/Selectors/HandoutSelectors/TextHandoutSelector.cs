using HalloweenSystem.GameLogic.GameObjects;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.HandoutSelectors;

/// <summary>
/// Represents a selector that selects handouts based on their text content.
/// </summary>
/// <param name="text">The text content to match in the handouts.</param>
public class TextHandoutSelector(string text) : Selector<Handout>
{
	/// <summary>
	/// Evaluates the selector in the given context and returns a collection of handouts that match the specified text content.
	/// </summary>
	/// <param name="context">The context in which to evaluate the selector.</param>
	/// <returns>A collection of handouts that match the specified text content.</returns>
	public override IEnumerable<Handout> Evaluate(Context context)
	{
		return new List<Handout> { new Handout(text) };
	}
}