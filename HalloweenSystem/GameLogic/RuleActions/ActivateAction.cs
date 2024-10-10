using HalloweenSystem.GameLogic.RuleSelectors;
using HalloweenSystem.GameLogic.Selectors;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.RuleActions;

public class ActivateAction(Selector<Rule> ruleSelector) : RuleAction
{
	public override void Evaluate(Context context)
	{
		ruleSelector.Evaluate(context).ToList().ForEach(rule => rule.Evaluate(context));
	}
}