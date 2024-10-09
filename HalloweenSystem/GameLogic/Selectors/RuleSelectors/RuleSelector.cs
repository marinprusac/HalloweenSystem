using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.RuleSelectors;

public abstract class RuleSelector
{
	public abstract IEnumerable<Rule> Evaluate(Context context);
}