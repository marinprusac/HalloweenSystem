using HalloweenSystem.GameLogic.TagSelectors;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.Selectors.TagSelectors;

public class SourceTagSelector : TagSelector
{
	public override IEnumerable<AssignedTag> Evaluate(Context context, Player? operatedPlayer = null)
	{
		throw new NotImplementedException();
	}
}