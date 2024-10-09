using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.HandoutParts;

public class TextHandoutPart(string Text) : HandoutPart
{
	public override string Evaluate(Context context, Player? iteratedPlayer = null, TagType? iteratedTagType = null)
	{
		return Text;
	}
}