using HalloweenSystem.GameLogic.RuleSelectors;
using HalloweenSystem.GameLogic.Selectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.RuleActions;

public class ActivateAction(Selector<Rule> ruleSelector) : RuleAction
{
	public override void Evaluate(Context context)
	{
		ruleSelector.Evaluate(context).ToList().ForEach(rule => rule.Evaluate(context));
	}
}