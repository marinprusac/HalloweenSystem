using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.Selectors.PlayerSelectors;

public class ExtractPlayerSelector(Selector<Tag> nestedSelector) : Selector<Player>
{
	public override IEnumerable<Player> Evaluate(Context context)
	{
		var tags = nestedSelector.Evaluate(context);

		var playerParameters = tags.Select(tag => tag.PlayerParameters.AsEnumerable());

		return GameObject.Union<Player>(playerParameters).Cast<Player>();
	}
}