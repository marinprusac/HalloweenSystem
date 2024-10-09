using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.HandoutParts;

public class TextHandoutPart(string text) : HandoutPart
{
	public override string Evaluate(Context context)
	{
		return text;
	}
}