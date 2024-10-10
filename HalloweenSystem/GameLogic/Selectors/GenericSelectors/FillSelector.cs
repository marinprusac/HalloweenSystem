using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors;

public class FillSelector<T>(string parameterName) : Selector<T> where T : GameObject, new()
{
	public override IEnumerable<T> Evaluate(Context context)
	{
		return [context.GetIteratingObject<T>(parameterName)];
	}
}