using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.TagSelectors;

public class EmptyTagSelector : TagSelector

{
	public override IEnumerable<AssignedTag> Evaluate(Context context, Player? operatedPlayer = null)
	{
		return [];
	}
}