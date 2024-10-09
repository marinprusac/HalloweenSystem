using HalloweenSystem.GameLogic.Selectors;
using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Selectors.TagSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.RuleActions;

public class AssignAction(bool assignTogether, Selector<Player> playerSelector, Selector<Tag> tagSelector)
	: RuleAction
{

	public AssignAction(Selector<Player> playerSelector, Selector<Tag> tagSelector) : this(false, playerSelector, tagSelector)
	{
	}


	private void EvaluateTogether(Context context)
	{
		var players = playerSelector.Evaluate(context).ToList();
		var tags = tagSelector.Evaluate(context).ToList();
		foreach (var player in players)
		{
			player.AssignTags(tags);
		}
	}

	private void EvaluateSeparate(Context context)
	{
		var players = playerSelector.Evaluate(context);
		foreach (var player in players)
		{
			context.CurrentPlayer = player;
			var tags = tagSelector.Evaluate(context);
			player.AssignTags(tags);
		}
	}
	
	public override void Evaluate(Context context)
	{
		if(assignTogether) EvaluateTogether(context);
		else EvaluateSeparate(context);
	}
}