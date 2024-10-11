using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HalloweenSystem.GameLogic.Selectors;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Utilities.Iterators;

/// <summary>
/// Represents an iterator that evaluates a context and returns a list of results of type TG.
/// </summary>
/// <typeparam name="T">The type of the iterable object, either Player or AssignedTag.</typeparam>
/// <typeparam name="TG">The return type of the iterator.</typeparam>
/// <param name="name">The name of the iterator.</param>
/// <param name="selector">The selector that evaluates to a collection of objects of type T.</param>
/// <param name="evaluator">The evaluator that evaluates the context and returns a result of type TG.</param>
public class Iterator<T, TG> (string name, Selector<T> selector, IEvaluator<TG> evaluator) : IEvaluator<List<TG>> where T : GameObject
{
    /// <summary>
    /// Gets the name of the iterator.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Gets the selector that evaluates to a collection of objects of type T.
    /// </summary>
    private Selector<T> Selector { get; } = selector;

    /// <summary>
    /// Gets the evaluator that evaluates the context and returns a result of type TG.
    /// </summary>
    private IEvaluator<TG> Evaluator { get; } = evaluator;

    /// <summary>
    /// Evaluates the selector in the given context and returns a list of results of type TG.
    /// </summary>
    /// <param name="context">The context in which to evaluate the selector.</param>
    /// <returns>A list of results of type TG.</returns>
    public List<TG> Evaluate(Context context)
    {
        var parameters = Selector.Evaluate(context);
        var returnValue = new List<TG>();

        foreach (var parameter in parameters)
        {
            context.Parameters[Name] = parameter;
            var evaluatedValue = Evaluator.Evaluate(context);
            returnValue.Add(evaluatedValue);
        }

        return returnValue;
    }
}