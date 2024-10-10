using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic;

public interface IEvaluator<out T>
{
	T Evaluate(Context context);
	
}