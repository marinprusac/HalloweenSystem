using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.RuleSelectors;

public class SingleRuleSelector(Rule rule) : RuleSelector
{
	public override IEnumerable<Rule> Evaluate(Context context)
	{
		return [rule];
	}
}