using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.RuleActions;

public abstract class RuleAction
{
	public abstract void Evaluate(Context context);
}