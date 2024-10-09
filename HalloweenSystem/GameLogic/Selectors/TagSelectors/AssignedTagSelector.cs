using HalloweenSystem.GameLogic.PlayerSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.TagSelectors;

public class AssignedTagSelector(TagType type, PlayerSelector? playerSelector = null, TagSelector? tagSelector = null)
	: TagSelector
{
	private readonly PlayerSelector _playerSelector = playerSelector ?? new EmptyPlayerSelector();
	private readonly TagSelector _tagSelector = tagSelector ?? new EmptyTagSelector();
	public override IEnumerable<AssignedTag> Evaluate(Context context, Player? operatedPlayer = null)
	{
		var players = _playerSelector.Evaluate(context, operatedPlayer);
		var tags = _tagSelector.Evaluate(context, operatedPlayer);
		var types = tags.Select(t => t.Type);
		
		return [new AssignedTag(type, players, types)];
	}
}