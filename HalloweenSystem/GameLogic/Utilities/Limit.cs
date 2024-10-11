using System;
using System.Collections.Generic;
using System.Linq;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Utilities;

/// <summary>
/// Represents a utility class that limits the number of objects of type T based on a specified amount.
/// </summary>
/// <typeparam name="T">The type of the objects to be limited, which must be a GameObject.</typeparam>
/// <param name="amount">The amount to limit the objects by, either as a percentage or a fixed number.</param>
public class Limit<T>(string amount) where T : GameObject
{
    /// <summary>
    /// Evaluates the given collection of objects and returns a limited collection based on the specified amount.
    /// </summary>
    /// <param name="objects">The collection of objects to be limited.</param>
    /// <returns>A limited collection of objects of type T.</returns>
    /// <exception cref="ArgumentException">Thrown when the amount is not a valid percentage or number.</exception>
    public IEnumerable<T> Evaluate(IEnumerable<T> objects)
    {
        var enumerable = objects.ToList();
        var count = enumerable.Count;
        int selectedCount;
        if (amount.Contains('%'))
        {
            var success = int.TryParse(amount.Replace("%", ""), out var result);
            if(success) selectedCount = (int)Math.Ceiling(count * (result / 100f));
            else throw new ArgumentException();
        }
        else
        {
            var success = int.TryParse(amount, out var result);
            if (success) selectedCount = result;
            else throw new ArgumentException();
        }

        var random = new Random();
        var selected = enumerable.ToList();
        for(int i = 0; i < count - selectedCount; i++)
        {
            selected.RemoveAt(random.Next(selected.Count));
        }

        return selected;
    }
}