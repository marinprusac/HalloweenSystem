using System.Collections.Generic;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.RuleSelectors;

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