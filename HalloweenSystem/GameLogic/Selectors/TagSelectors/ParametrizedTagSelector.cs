using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.Selectors.TagSelectors;

public class ParametrizedTagSelector(string type, Selector<Player>? playerSelector = null, Selector<Tag>? tagSelector = null) : Selector<Tag>
{
	private readonly Selector<Player> _playerSelector = playerSelector ?? new EmptySelector<Player>();
	private readonly Selector<Tag> _tagSelector = tagSelector ?? new EmptySelector<Tag>();
	public override IEnumerable<Tag> Evaluate(Context context)
	{
		var players = _playerSelector.Evaluate(context);
		var tags = _tagSelector.Evaluate(context);
		var types = tags.Select(t => t.Name);
		
		return [new Tag(type, players, types)];
	}
}