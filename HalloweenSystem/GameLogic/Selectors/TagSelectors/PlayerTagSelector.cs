using System.Collections.Generic;
using System.Linq;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.TagSelectors;

public class PlayerTagSelector(Selector<Tag> tagSelector, Selector<Player>? playerSelector=null) : Selector<Tag>
{
	private Selector<Player>? _playerSelector = playerSelector;

	public override IEnumerable<Tag> Evaluate(Context context)
	{
		_playerSelector ??= new AllSelector<Player>();
		
		var players =  _playerSelector.Evaluate(context);
		
		var tags = tagSelector.Evaluate(context);
		var playerTags = players.Select(player => player.AssignedTags.AsEnumerable());
		var joinedTags = GameObject.Union<Tag>(playerTags).Cast<Tag>();

		var result = joinedTags.Where(jt => tags.Any(jt.Covers));
		return result.Cast<Tag>();
	}
}