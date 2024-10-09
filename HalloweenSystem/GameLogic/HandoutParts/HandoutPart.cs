using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.HandoutParts;

public abstract class HandoutPart
{
	public abstract string Evaluate(Context context, Player? iteratedPlayer = null, TagType? iteratedTagType = null);
}