using HalloweenSystem.GameLogic.PlayerSelectors;
using HalloweenSystem.GameLogic.TagSelectors;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.RuleActions;

public class AssignAction(bool assignTogether, PlayerSelector playerSelector, TagSelector tagSelector)
	: RuleAction
{

	public AssignAction(PlayerSelector playerSelector, TagSelector tagSelector) : this(false, playerSelector, tagSelector)
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
			var tags = tagSelector.Evaluate(context, player);
			player.AssignTags(tags);
		}
	}
	
	public override void Evaluate(Context context)
	{
		if(assignTogether) EvaluateTogether(context);
		else EvaluateSeparate(context);
	}
}