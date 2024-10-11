using System.Collections.Generic;
using System.Linq;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Selectors.TagSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.PlayerSelectors;

public class HasTagsPlayerSelector(Selector<Tag> tagSelector, Selector<Player>? playerSelector = null)
	: Selector<Player>
{
	private Selector<Player>? _playerSelector = playerSelector;
	
	public override IEnumerable<Player> Evaluate(Context context)
	{
		_playerSelector ??= new AllSelector<Player>();
		
		var players = _playerSelector?.Evaluate(context) ?? [];
		var tags = tagSelector.Evaluate(context) ?? [];
		
		return players.Where(player => tags.All(tag => player.GetQueriedTag(tag, out _)));

	}
}