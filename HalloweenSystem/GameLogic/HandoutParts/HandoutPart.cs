using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.HandoutParts;

public abstract class HandoutPart : IEvaluator<string>
{
	public abstract string Evaluate(Context context);
}