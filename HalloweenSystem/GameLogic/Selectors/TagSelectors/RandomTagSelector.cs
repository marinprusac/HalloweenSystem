using HalloweenSystem.GameLogic.Utilities;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.TagSelectors;

public class RandomTagSelector(Limit<AssignedTag> limit, TagSelector nestedTagSelector) : TagSelector
{
	public override IEnumerable<AssignedTag> Evaluate(Context context, Player? operatedPlayer = null)
	{
		var tags = nestedTagSelector.Evaluate(context, operatedPlayer);
		return limit.Evaluate(tags);
	}
}
