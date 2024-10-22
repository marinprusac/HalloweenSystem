using System.Collections.Generic;
using System.Linq;
using HalloweenSystem.GameLogic.GameObjects;

namespace HalloweenSystem.GameLogic.Settings;

/// <summary>
/// Represents a player in the game logic.
/// </summary>
/// <param name="name">The name of the player.</param>
public class Player(string name) : GameObject(name)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Player"/> class with a name and assigned tags.
    /// </summary>
    /// <param name="name">The name of the player.</param>
    /// <param name="assignedTags">The collection of tags assigned to the player.</param>
    public Player(string name, IEnumerable<Tag> assignedTags) : this(name)
    {
        AssignedTags = assignedTags.ToList();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Player"/> class with a default name.
    /// </summary>
    public Player() : this("")
    {
    }

    /// <summary>
    /// Gets the collection of handouts assigned to the player.
    /// </summary>
    public List<Handout> Handouts { get; } = [];

    /// <summary>
    /// Gets the collection of tags assigned to the player.
    /// </summary>
    public List<Tag> AssignedTags { get; } = [];

    /// <summary>
    /// Retrieves a tag of the specified type assigned to the player.
    /// </summary>
    /// <param name="type">The type of the tag.</param>
    /// <param name="tag">The tag of the specified type, if found.</param>
    /// <returns><c>true</c> if a tag of the specified type is found; otherwise, <c>false</c>.</returns>
    public bool GetTagOfType(string type, out Tag? tag)
    {
        tag = AssignedTags.FirstOrDefault(tag => tag.Name == type);
        return tag != null;
    }

    /// <summary>
    /// Retrieves a tag that covers the specified query tag.
    /// </summary>
    /// <param name="queryTag">The query tag.</param>
    /// <param name="tag">The tag that covers the query tag, if found.</param>
    /// <returns><c>true</c> if a tag that covers the query tag is found; otherwise, <c>false</c>.</returns>
    public bool GetQueriedTag(Tag queryTag, out Tag? tag)
    {
        tag = AssignedTags.FirstOrDefault(tag => tag.Covers(queryTag));
        return tag != null;
    }

    /// <summary>
    /// Assigns new tags to the player, extending existing tags if they match by name.
    /// </summary>
    /// <param name="newTags">The collection of new tags to assign.</param>
    public void AssignTags(IEnumerable<Tag> newTags)
    {
        foreach (var newTag in newTags)
        {
            if (GetTagOfType(newTag.Name, out var assignedTag))
            {
                assignedTag?.Extend(newTag);
            }
            else
            {
                AssignedTags.Add(newTag.Copy());
            }
        }
    }

    /// <summary>
    /// Returns a string representation of the player.
    /// </summary>
    /// <returns>A string representation of the player.</returns>
    public override string ToString()
    {
        var text = Name + "\n";
        text += "Tags: ";
        text = AssignedTags.Aggregate(text, (current, tag) => current + (tag + ", "));
        if (AssignedTags.Count > 0)
            text = text[..^2];

        text += "\nHandouts:";

        text = Handouts.Aggregate(text, (current, handout) => current + "\n- " + handout);
        
        return text;
    }

    /// <summary>
    /// Retrieves all game objects associated with the player in the given context.
    /// </summary>
    /// <param name="context">The context in which to retrieve the game objects.</param>
    /// <returns>A collection of game objects associated with the player.</returns>
    protected override IEnumerable<GameObject> _everything(Context context)
    {
        return context.Players;
    }

    /// <summary>
    /// Retrieves the complement of the given set of game objects in the context.
    /// </summary>
    /// <param name="objectSet">The set of game objects to complement.</param>
    /// <param name="context">The context in which to retrieve the complement.</param>
    /// <returns>A collection of game objects that are not in the given set.</returns>
    protected override IEnumerable<GameObject> _complement(IEnumerable<GameObject> objectSet, Context context)
    {
        return context.Players.Except(objectSet);
    }
}