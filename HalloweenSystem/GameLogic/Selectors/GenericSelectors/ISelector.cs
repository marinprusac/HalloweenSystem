using System.Xml;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.GenericSelectors;

/// <summary>
/// A selector is a class that evaluates a context and returns a list of objects.
/// This class serves as a base class for all selectors.
/// </summary>
/// <typeparam name="T">type of the objects returned in a list after evaluation</typeparam>
public interface ISelector<out T> : IEvaluator<IEnumerable<T>> where T : GameObject;