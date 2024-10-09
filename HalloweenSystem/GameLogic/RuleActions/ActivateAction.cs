using HalloweenSystem.GameLogic.RuleSelectors;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.RuleActions;

public class ActivateAction(RuleSelector ruleSelector) : RuleAction
{
	public override void Evaluate(Context context)
	{
		ruleSelector.Evaluate(context).ToList().ForEach(rule => rule.Evaluate(context));
	}
}