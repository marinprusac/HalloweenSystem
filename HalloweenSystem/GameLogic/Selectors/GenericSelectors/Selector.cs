using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.Selectors;

public abstract class Selector<T> : IEvaluator<IEnumerable<T>> where T : GameObject
{
	public abstract IEnumerable<T> Evaluate(Context context);
}