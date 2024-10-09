using HalloweenSystem.GameLogic.PlayerSelectors;
using HalloweenSystem.GameLogic.RuleActions;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.Settings;

public class Rule(string name, List<RuleAction> actions)
{

	public Rule(string name, PlayerSelector requirement, List<RuleAction> actions) : this(name, actions)
	{
		_requirement = requirement;
	}

	public readonly string Name = name;
	private readonly PlayerSelector _requirement = new EveryPlayerSelector();

	public void Evaluate(Context context)
	{
		if (!_requirement.Evaluate(context).Any()) return;
		actions.ForEach(action => action.Evaluate(context));
	}
}