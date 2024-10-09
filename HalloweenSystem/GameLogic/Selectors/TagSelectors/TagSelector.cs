using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.TagSelectors;

public abstract class TagSelector
{
	public abstract IEnumerable<AssignedTag> Evaluate(Context context, Player? operatedPlayer = null);
}