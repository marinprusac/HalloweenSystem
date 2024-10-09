using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities.Iterators;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.HandoutParts;

public class IterableHandoutPart<T>(string joinString, Iterator<T, string> iterator) : HandoutPart where T : GameObject
{
	public override string Evaluate(Context context)
	{
		var strings = iterator.Evaluate(context);
		return string.Join(joinString, strings);
	}
}