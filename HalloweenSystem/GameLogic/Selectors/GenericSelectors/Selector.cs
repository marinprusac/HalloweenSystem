using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors;

public abstract class Selector<T> : IEvaluator<IEnumerable<T>> where T : GameObject
{
	public abstract IEnumerable<T> Evaluate(Context context);
}