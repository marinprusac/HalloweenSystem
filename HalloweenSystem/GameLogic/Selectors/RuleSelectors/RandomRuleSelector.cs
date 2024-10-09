using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.RuleSelectors;

public class RandomRuleSelector(Limit<Rule> limit, RuleSelector nestedRuleSelector) : RuleSelector
{
	public override IEnumerable<Rule> Evaluate(Context context)
	{
		return limit.Evaluate(nestedRuleSelector.Evaluate(context));
	}
}