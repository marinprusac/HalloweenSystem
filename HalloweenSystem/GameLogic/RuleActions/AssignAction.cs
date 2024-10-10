using HalloweenSystem.GameLogic.Selectors;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Selectors.TagSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.RuleActions;

public class AssignAction(bool assignTogether, Selector<Player> playerSelector, Selector<Tag> tagSelector)
	: RuleAction
{

	public AssignAction(Selector<Player> playerSelector, Selector<Tag> tagSelector) : this(false, playerSelector, tagSelector)
	{
	}
	
	public override void Evaluate(Context context)
	{
		var players = playerSelector.Evaluate(context);

		if (assignTogether)
		{
			var tags = tagSelector.Evaluate(context).ToList();
			foreach (var player in players)
			{
				player.AssignTags(tags);
			}
		}
		else
		{
			foreach (var player in players)
			{
				context.CurrentPlayer = player;
				var tags = tagSelector.Evaluate(context);
				player.AssignTags(tags);
			}
		}
		
	}
}