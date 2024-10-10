using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;

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