using HalloweenSystem.GameLogic.Selectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.RuleSelectors;

public class ListRuleSelector(IEnumerable<Selector<Rule>> nestedRuleSelectors) : Selector<Rule>
{
	public override IEnumerable<Rule> Evaluate(Context context)
	{
		List<Rule> rules = [];
		foreach (Selector<Rule> ruleSelector in nestedRuleSelectors)
		{
			var selectedRules = ruleSelector.Evaluate(context);
			rules.AddRange(selectedRules);
		}
		return rules;
	}
}