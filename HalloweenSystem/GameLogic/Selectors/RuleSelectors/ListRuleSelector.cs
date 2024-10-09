using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.RuleSelectors;

public class ListRuleSelector(IEnumerable<RuleSelector> nestedRuleSelectors) : RuleSelector
{
	public override IEnumerable<Rule> Evaluate(Context context)
	{
		List<Rule> rules = [];
		foreach (RuleSelector ruleSelector in nestedRuleSelectors)
		{
			var selectedRules = ruleSelector.Evaluate(context);
			rules.AddRange(selectedRules);
		}
		return rules;
	}
}