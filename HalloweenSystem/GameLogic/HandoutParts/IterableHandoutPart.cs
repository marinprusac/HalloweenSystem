using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities.Iterators;

namespace HalloweenSystem.GameLogic.HandoutParts;

public class IterableHandoutPart<T>(string joinString, string iterableName, Selector<T> selectorOfIterable,  HandoutPart nestedPart ) : HandoutPart where T : GameObject
{
	public override string Evaluate(Context context)
	{
		var iterator = new Iterator<T, string>(iterableName, selectorOfIterable, nestedPart);
		var strings = iterator.Evaluate(context);
		return string.Join(joinString, strings);
	}
}