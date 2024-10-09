using HalloweenSystem.GameLogic.HandoutParts;
using HalloweenSystem.GameLogic.Selectors;
using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.RuleActions;

public class HandoutAction(bool handoutTogether, Selector<Player> playerSelector, List<HandoutPart> handoutParts)
	: RuleAction
{

	public HandoutAction(Selector<Player> playerSelector, List<HandoutPart> handoutParts) : this(false, playerSelector, handoutParts)
	{
	}

	private Selector<Player> _playerSelector = playerSelector;
	private List<HandoutPart> _handoutParts = handoutParts;
	private bool _handoutTogether = handoutTogether;
	
	public override void Evaluate(Context context)
	{
		context.CurrentPlayer = null;
		var players = _playerSelector.Evaluate(context);

		if (_handoutTogether)
		{
			var handout = string.Join(" ", _handoutParts.Select(part => part.Evaluate(context)));
			foreach (var player in players)
			{
				player.Handouts.Add(handout);
			}
		}
		else
		{
			foreach (var player in players)
			{
				context.CurrentPlayer = player;
				var handout = string.Join(" ", _handoutParts.Select(part => part.Evaluate(context)));
				player.Handouts.Add(handout);
			}
		}
		

	}
}