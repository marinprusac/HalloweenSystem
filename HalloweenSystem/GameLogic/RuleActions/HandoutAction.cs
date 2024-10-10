using HalloweenSystem.GameLogic.HandoutParts;
using HalloweenSystem.GameLogic.Selectors;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.RuleActions;

public class HandoutAction(bool handoutTogether, Selector<Player> playerSelector, HandoutPart handoutPart)
	: RuleAction
{

	public HandoutAction(Selector<Player> playerSelector, HandoutPart handoutPart) : this(false, playerSelector, handoutPart)
	{
	}

	public override void Evaluate(Context context)
	{
		context.CurrentPlayer = null;
		var players = playerSelector.Evaluate(context);

		if (handoutTogether)
		{
			var text = handoutPart.Evaluate(context);
			foreach (var player in players)
			{
				player.Handouts.Add(text);
			}
		}
		else
		{
			foreach (var player in players)
			{
				context.CurrentPlayer = player;
				var text = handoutPart.Evaluate(context);
				player.Handouts.Add(text);
			}
		}
		

	}
}