using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic;

public interface IEvaluator<out T>
{
	T Evaluate(Context context);
	
}