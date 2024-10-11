using System.Collections.Generic;
using HalloweenSystem.GameLogic.Selectors;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.RuleSelectors;

public class SingleRuleSelector(Rule rule) : Selector<Rule>
{
	public override IEnumerable<Rule> Evaluate(Context context)
	{
		return [rule];
	}
}