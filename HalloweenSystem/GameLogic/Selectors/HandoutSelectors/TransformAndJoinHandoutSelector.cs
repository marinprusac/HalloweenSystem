using HalloweenSystem.GameLogic.GameObjects;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.HandoutSelectors;

/// <summary>
/// Represents a selector that transforms and joins handouts based on the evaluation of a nested selector.
/// </summary>
/// <typeparam name="TN">The type of game object that the nested selector evaluates.</typeparam>
/// <param name="joinString">The string used to join the text representations of the selected game objects.</param>
/// <param name="placeholder">The placeholder text to use if no game objects are selected.</param>
/// <param name="nestedSelector">The nested selector that evaluates to a collection of game objects.</param>
public class TransformAndJoinHandoutSelector<TN>(string joinString, string placeholder, Selector<TN> nestedSelector) : Selector<Handout> where TN : GameObject
{
	/// <summary>
	/// Evaluates the selector in the given context and returns a collection of handouts with the joined text content.
	/// </summary>
	/// <param name="context">The context in which to evaluate the selector.</param>
	/// <returns>A collection of handouts with the joined text content.</returns>
	public override IEnumerable<Handout> Evaluate(Context context)
	{
		var text = placeholder;
		var objects = nestedSelector.Evaluate(context);
		var texts = objects.Select(obj => obj.ToHandoutText()).ToList();
		if (texts.Count != 0)
		{
			text = string.Join(joinString, texts);
		}
		var handout = new Handout(text);
		return new List<Handout> { handout };
	}
}