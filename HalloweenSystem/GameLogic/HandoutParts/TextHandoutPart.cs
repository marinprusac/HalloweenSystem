using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.HandoutParts;

public class TextHandoutPart(string text) : HandoutPart
{
	public override string Evaluate(Context context)
	{
		return text;
	}
}