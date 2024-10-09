using HalloweenSystem.GameLogic.RuleActions;
using HalloweenSystem.GameLogic.Selectors;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.Settings;

public class Rule(string name, List<RuleAction> actions) : GameObject(name)
{

	public Rule(string name, Selector<Player> requirement, List<RuleAction> actions) : this(name, actions)
	{
		_requirement = requirement;
	}

	public Rule() : this("", [])
	{
		
	}

	private readonly Selector<Player> _requirement = new EverySelector<Player>();

	public void Evaluate(Context context)
	{
		if (!_requirement.Evaluate(context).Any()) return;
		actions.ForEach(action => action.Evaluate(context));
	}

	protected override IEnumerable<GameObject> _everything(Context context)
	{
		return context.Setting.Rules;
	}

	protected override IEnumerable<GameObject> _complement(IEnumerable<GameObject> objectSet, Context context)
	{
		return context.Setting.Rules.Except(objectSet);
	}
}