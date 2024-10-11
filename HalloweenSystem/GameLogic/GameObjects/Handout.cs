using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.GameObjects;

/// <summary>
/// Represents a handout in the game logic.
/// </summary>
/// <param name="text">The text content of the handout.</param>
public class Handout(string text) : GameObject
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Handout"/> class with default text.
	/// </summary>
	public Handout() : this("")
	{}

	/// <summary>
	/// Gets the text content of the handout.
	/// </summary>
	private string Text { get; } = text;

	/// <summary>
	/// Converts the handout to its text representation.
	/// </summary>
	/// <returns>The text content of the handout.</returns>
	public override string ToHandoutText()
	{
		return Text;
	}

	/// <summary>
	/// Returns a string representation of the handout.
	/// </summary>
	/// <returns>The text content of the handout.</returns>
	public override string ToString()
	{
		return Text;
	}
}