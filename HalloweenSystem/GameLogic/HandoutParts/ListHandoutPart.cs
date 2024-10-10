using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.HandoutParts;

public class ListHandoutPart(List<HandoutPart> nestedParts) : HandoutPart
{
	public override string Evaluate(Context context)
	{
		return nestedParts.Aggregate("", (current, part) => current + part.Evaluate(context));
	}
}