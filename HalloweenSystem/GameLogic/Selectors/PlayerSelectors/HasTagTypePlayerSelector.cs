using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.PlayerSelectors;

public class HasTagTypePlayerSelector(string type, Selector<Player>? playerSelector=null) : Selector<Player>
{
	private Selector<Player>? _playerSelector = playerSelector;
	public override IEnumerable<Player> Evaluate(Context context)
	{
		_playerSelector ??= new EverySelector<Player>();
		
		var players = _playerSelector.Evaluate(context);
		var filtered = players.Where(p => p.AssignedTags.Any(t => t.Name == type));
		return filtered;
	}
}