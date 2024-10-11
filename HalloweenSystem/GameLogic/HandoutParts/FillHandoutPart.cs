using System;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.HandoutParts;

public class FillHandoutPart(string parameterName) : HandoutPart
{
	public override string Evaluate(Context context)
	{
		if(context.IteratingObjects.TryGetValue(parameterName, out var iteratingObject))
			return iteratingObject.Name;
		
		throw new ArgumentException($"Parameter {parameterName} not found in context.");
	}
}