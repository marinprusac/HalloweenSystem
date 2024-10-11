using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HalloweenSystem.GameLogic.Selectors;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Utilities.Iterators;

/// <summary>
/// 
/// </summary>
/// <param name="name"></param>
/// <param name="iterateTogether"></param>
/// <param name="selector"></param>
/// <param name="evaluator"></param>
/// <typeparam name="T">type of iterable object, either Player or AssignedTag</typeparam>
/// <typeparam name="TG">return type of iterator</typeparam>
public class Iterator<T, TG> (string name, Selector<T> selector, IEvaluator<TG> evaluator) : IEvaluator<List<TG>> where T : GameObject
{
	
	public string Name { get; } = name;
	private Selector<T> Selector { get; } = selector;
	private IEvaluator<TG> Evaluator { get; } = evaluator;
	



	public List<TG> Evaluate(Context context)
	{
		var parameters = Selector.Evaluate(context);
		
		var returnValue = new List<TG>();
		
		
		context.IteratingOrder.Add(Name);
		
		foreach (var parameter in parameters)
		{
			context.IteratingObjects[Name] = parameter;;
			
			var evaluatedValue = Evaluator.Evaluate(context);
			returnValue.Add(evaluatedValue);
			
		}


		

		return returnValue;
	}
}