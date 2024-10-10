using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.HandoutParts;

public abstract class HandoutPart : IEvaluator<string>
{
	public abstract string Evaluate(Context context);
}