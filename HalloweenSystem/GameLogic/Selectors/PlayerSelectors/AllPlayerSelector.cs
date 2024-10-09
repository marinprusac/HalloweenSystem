using HalloweenSystem.GameLogic.PlayerSelectors;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.PlayerSelectors;

public class AllPlayerSelector : PlayerSelector
{

	public AllPlayerSelector(IEnumerable<PlayerSelector> playerSelectors)
	{
		_playerSelectors = playerSelectors;
	}

	public AllPlayerSelector(bool globalBoolean, IEnumerable<PlayerSelector> playerSelectors)
	{
		_globalBoolean = globalBoolean;
		_playerSelectors = playerSelectors;
	}

	private readonly bool _globalBoolean = false;
	private readonly IEnumerable<PlayerSelector> _playerSelectors;
	
	public override IEnumerable<Player> Evaluate(Context context, Player? operatedPlayer = null)
	{
		if (_globalBoolean)
			return _playerSelectors.Any(selector => !selector.Evaluate(context, operatedPlayer).Any()) ? [] : context.Players;
		
		var players = context.Players.ToList();
		players = _playerSelectors.Aggregate(players, (current, playerSelector)
			=> current.Intersect(playerSelector.Evaluate(context, operatedPlayer)).ToList());

		return players ?? throw new InvalidOperationException();
	}
}