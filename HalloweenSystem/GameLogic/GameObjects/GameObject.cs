using System;
using System.Collections.Generic;
using System.Linq;

namespace HalloweenSystem.GameLogic.Settings;

/// <summary>
/// Represents a game object with a name and provides methods for set operations on game objects.
/// </summary>
/// <param name="name">The name of the game object.</param>
public class GameObject(string name = "") : IEquatable<GameObject>
{
    /// <summary>
    /// Gets the name of the game object.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Performs a union operation on the given sets of game objects.
    /// </summary>
    /// <typeparam name="T">The type of game object.</typeparam>
    /// <param name="objectSets">The sets of game objects to union.</param>
    /// <returns>A collection of game objects that are in any of the given sets.</returns>
    public static IEnumerable<GameObject> Union<T>(IEnumerable<IEnumerable<GameObject>> objectSets)
        where T : GameObject, new()
    {
        var obj = new T();
        return obj._union(objectSets);
    }

    /// <summary>
    /// Performs an intersection operation on the given sets of game objects.
    /// </summary>
    /// <typeparam name="T">The type of game object.</typeparam>
    /// <param name="objectSets">The sets of game objects to intersect.</param>
    /// <returns>A collection of game objects that are in all of the given sets.</returns>
    public static IEnumerable<GameObject> Intersect<T>(IEnumerable<IEnumerable<GameObject>> objectSets)
        where T : GameObject, new()
    {
        var obj = new T();
        return obj._intersect(objectSets);
    }

    /// <summary>
    /// Retrieves all game objects associated with the given context.
    /// </summary>
    /// <typeparam name="T">The type of game object.</typeparam>
    /// <param name="context">The context in which to retrieve the game objects.</param>
    /// <returns>A collection of all game objects in the given context.</returns>
    public static IEnumerable<GameObject> Everything<T>(Context context) where T : GameObject, new()
    {
        var obj = new T();
        return obj._everything(context);
    }

    /// <summary>
    /// Retrieves the complement of the given set of game objects in the context.
    /// </summary>
    /// <typeparam name="T">The type of game object.</typeparam>
    /// <param name="objectSet">The set of game objects to complement.</param>
    /// <param name="context">The context in which to retrieve the complement.</param>
    /// <returns>A collection of game objects that are not in the given set.</returns>
    public static IEnumerable<GameObject> Complement<T>(IEnumerable<GameObject> objectSet, Context context)
        where T : GameObject, new()
    {
        var obj = new T();
        return obj._complement(objectSet, context);
    }

    /// <summary>
    /// Retrieves all game objects associated with the given context.
    /// </summary>
    /// <param name="context">The context in which to retrieve the game objects.</param>
    /// <returns>A collection of all game objects in the given context.</returns>
    protected virtual IEnumerable<GameObject> _everything(Context context)
    {
        throw new Exception("Everything is not defined for this object");
    }

    /// <summary>
    /// Retrieves the complement of the given set of game objects in the context.
    /// </summary>
    /// <param name="objectSet">The set of game objects to complement.</param>
    /// <param name="context">The context in which to retrieve the complement.</param>
    /// <returns>A collection of game objects that are not in the given set.</returns>
    protected virtual IEnumerable<GameObject> _complement(IEnumerable<GameObject> objectSet, Context context)
    {
        throw new Exception("Complement is not defined for this object");
    }

    /// <summary>
    /// Performs a union operation on the given sets of game objects.
    /// </summary>
    /// <param name="objectSets">The sets of game objects to union.</param>
    /// <returns>A collection of game objects that are in any of the given sets.</returns>
    protected virtual IEnumerable<GameObject> _union(IEnumerable<IEnumerable<GameObject>> objectSets)
    {
        var objectSetsList = objectSets.ToList();
        var result = new List<GameObject>();
        foreach (var objectSet in objectSetsList)
        {
            foreach (var gameObject in objectSet)
            {
                if (result.Any(g => g.Equals(gameObject))) continue;
                result.Add(gameObject);
            }
        }

        return result;
    }

    /// <summary>
    /// Performs an intersection operation on the given sets of game objects.
    /// </summary>
    /// <param name="objectSets">The sets of game objects to intersect.</param>
    /// <returns>A collection of game objects that are in all of the given sets.</returns>
    /// <exception cref="ArgumentException">Thrown if the list of object sets is empty.</exception>
    protected virtual IEnumerable<GameObject> _intersect(IEnumerable<IEnumerable<GameObject>> objectSets)
    {
        var objectSetsList = objectSets.ToList();

        if (objectSetsList.Count == 0) throw new ArgumentException("Cannot intersect empty list of object sets");

        var result = objectSetsList[0].ToList();

        foreach (var obj in from obj in result.ToList()
                            from objectSet in objectSetsList.Where(objectSet => !objectSet.Any(o => o.Equals(obj)))
                            select obj)
        {
            result.Remove(obj);
        }

        return result;
    }

    /// <summary>
    /// Determines whether the current game object is equal to another game object.
    /// </summary>
    /// <param name="other">The other game object to compare with.</param>
    /// <returns><c>true</c> if the current game object is equal to the other game object; otherwise, <c>false</c>.</returns>
    public bool Equals(GameObject? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name;
    }

    /// <summary>
    /// Determines whether the current game object is equal to another object.
    /// </summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns><c>true</c> if the current game object is equal to the other object; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((GameObject)obj);
    }

    /// <summary>
    /// Returns the hash code for the current game object.
    /// </summary>
    /// <returns>The hash code for the current game object.</returns>
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    /// <summary>
    /// Determines whether two game objects are equal.
    /// </summary>
    /// <param name="left">The first game object to compare.</param>
    /// <param name="right">The second game object to compare.</param>
    /// <returns><c>true</c> if the two game objects are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(GameObject? left, GameObject? right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Determines whether two game objects are not equal.
    /// </summary>
    /// <param name="left">The first game object to compare.</param>
    /// <param name="right">The second game object to compare.</param>
    /// <returns><c>true</c> if the two game objects are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(GameObject? left, GameObject? right)
    {
        return !Equals(left, right);
    }

    /// <summary>
    /// Returns a string representation of the game object.
    /// </summary>
    /// <returns>A string representation of the game object.</returns>
    public override string ToString()
    {
        return Name;
    }

    /// <summary>
    /// Converts the game object to its handout text representation.
    /// </summary>
    /// <returns>The handout text representation of the game object.</returns>
    public virtual string ToHandoutText()
    {
        return Name;
    }
}