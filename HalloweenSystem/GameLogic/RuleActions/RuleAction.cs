using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.RuleActions;

public abstract class RuleAction
{
	public abstract void Evaluate(Context context);
}