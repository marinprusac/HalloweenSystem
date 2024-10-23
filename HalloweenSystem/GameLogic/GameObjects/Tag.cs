using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.GameObjects;

/// <summary>
/// Represents a tag in the game logic, which can be associated with players and other tags.
/// </summary>
/// <param name="name">The name of the tag.</param>
/// <param name="players">The collection of players associated with the tag.</param>
/// <param name="tags">The collection of tags associated with the tag.</param>
public class Tag(string name = "", IEnumerable<Player>? players = null, IEnumerable<string>? tags = null) : GameObject(name)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tag"/> class with a default name.
    /// </summary>
    public Tag() : this("")
    {
    }

    /// <summary>
    /// Gets the collection of players associated with the tag.
    /// </summary>
    public HashSet<Player> PlayerParameters { get; } = (players ?? []).ToHashSet();

    /// <summary>
    /// Gets the collection of tags associated with the tag.
    /// </summary>
    public HashSet<string> TagTypeParameters { get; } = (tags ?? []).ToHashSet();

    /// <summary>
    /// Retrieves all game objects associated with the tag in the given context.
    /// </summary>
    /// <param name="context">The context in which to retrieve the game objects.</param>
    /// <returns>A collection of game objects associated with the tag.</returns>
    protected override IEnumerable<GameObject> _everything(Context context)
    {
        var tags = context.Setting.Tags ?? throw new ArgumentException("Tags are not defined in the setting");
        return tags.Select(tag => new Tag(tag));
    }

    /// <summary>
    /// Retrieves the complement of the given set of game objects in the context.
    /// </summary>
    /// <param name="objectSet">The set of game objects to complement.</param>
    /// <param name="context">The context in which to retrieve the complement.</param>
    /// <returns>A collection of game objects that are not in the given set.</returns>
    protected override IEnumerable<GameObject> _complement(IEnumerable<GameObject> objectSet, Context context)
    {
        var everything = _everything(context);
        return everything.Except(objectSet);
    }

    /// <summary>
    /// Retrieves the union of the given sets of game objects.
    /// </summary>
    /// <param name="objectSets">The sets of game objects to union.</param>
    /// <returns>A collection of game objects that are in any of the given sets.</returns>
    protected override IEnumerable<GameObject> _union(IEnumerable<IEnumerable<GameObject>> objectSets)
    {
        var tagSets = objectSets.Select(set => set.Cast<Tag>()).ToList();
        var result = new List<Tag>();

        foreach (var tagSet in tagSets)
        {
            foreach (var tag in tagSet)
            {
                var duplicatedTag = result.Find(t => t.Equals(tag));

                if (duplicatedTag == null)
                {
                    result.Add(tag.Copy());
                }
                else
                {
                    duplicatedTag.Extend(tag);
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Retrieves the intersection of the given sets of game objects.
    /// </summary>
    /// <param name="objectSets">The sets of game objects to intersect.</param>
    /// <returns>A collection of game objects that are in all of the given sets.</returns>
    /// <exception cref="ArgumentException">Thrown if the list of object sets is empty.</exception>
    protected override IEnumerable<GameObject> _intersect(IEnumerable<IEnumerable<GameObject>> objectSets)
    {
        var tagSets = objectSets.Select(set => set.Cast<Tag>()).ToList();

        if (tagSets.Count == 0) throw new ArgumentException("Cannot intersect empty list of object sets");

        var result = tagSets[0].ToList();
        var result2 = new List<Tag>();

        for (int i = 1; i < tagSets.Count; i++)
        {
            var tagSet = tagSets[i];
            foreach (var tag in tagSet)
            {
                var duplicatedTag = result.Find(t => t.Equals(tag));
                if (duplicatedTag == null) continue;
                duplicatedTag.Restrict(tag);
                result2.Add(duplicatedTag.Copy());
            }
            result = result2;
        }

        return result;
    }

    /// <summary>
    /// Extends the current tag with the parameters of the given tag.
    /// </summary>
    /// <param name="tag">The tag to extend with.</param>
    /// <exception cref="ArgumentException">Thrown if the tag names do not match.</exception>
    public void Extend(Tag tag)
    {
        if (tag.Name != Name)
            throw new ArgumentException("Cannot update tag with different type");
        PlayerParameters.UnionWith(tag.PlayerParameters);
        TagTypeParameters.UnionWith(tag.TagTypeParameters);
    }

    /// <summary>
    /// Restricts the current tag with the parameters of the given tag.
    /// </summary>
    /// <param name="tag">The tag to restrict with.</param>
    /// <exception cref="ArgumentException">Thrown if the tag names do not match.</exception>
    public void Restrict(Tag tag)
    {
        if (tag.Name != Name)
            throw new ArgumentException("Cannot update tag with different type");
        PlayerParameters.IntersectWith(tag.PlayerParameters);
        TagTypeParameters.IntersectWith(tag.TagTypeParameters);
    }

    /// <summary>
    /// Determines whether the current tag covers the given tag.
    /// </summary>
    /// <param name="tag">The tag to check coverage against.</param>
    /// <returns><c>true</c> if the current tag covers the given tag; otherwise, <c>false</c>.</returns>
    public bool Covers(Tag tag)
    {
        return Name ==
               tag.Name
               && PlayerParameters.IsSupersetOf(tag.PlayerParameters)
               && TagTypeParameters.IsSupersetOf(tag.TagTypeParameters);
    }

    /// <summary>
    /// Creates a copy of the current tag.
    /// </summary>
    /// <returns>A new tag that is a copy of the current tag.</returns>
    public Tag Copy()
    {
        return new Tag(Name, PlayerParameters, TagTypeParameters);
    }

    /// <summary>
    /// Multiplies two tags, restricting the first tag with the parameters of the second tag.
    /// </summary>
    /// <param name="tag1">The first tag.</param>
    /// <param name="tag2">The second tag.</param>
    /// <returns>A new tag that is the result of the multiplication.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the tag names do not match.</exception>
    public static Tag operator*(Tag tag1, Tag tag2)
    {
        if (tag1.Name != tag2.Name) throw new InvalidOperationException();
        var newTag = tag1.Copy();
        newTag.Restrict(tag2);
        return newTag;
    }

    /// <summary>
    /// Adds two tags, extending the first tag with the parameters of the second tag.
    /// </summary>
    /// <param name="tag1">The first tag.</param>
    /// <param name="tag2">The second tag.</param>
    /// <returns>A new tag that is the result of the addition.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the tag names do not match.</exception>
    public static Tag operator+(Tag tag1, Tag tag2)
    {
        if (tag1.Name != tag2.Name) throw new InvalidOperationException();
        var newTag = tag1.Copy();
        newTag.Extend(tag2);
        return newTag;
    }

    /// <summary>
    /// Returns a string representation of the tag.
    /// </summary>
    /// <returns>A string representation of the tag.</returns>
    public override string ToString()
    {
        var text = Name;

        if (PlayerParameters.Count > 0 || TagTypeParameters.Count > 0)
        {
            text += "[";

            text += string.Join(", ", PlayerParameters.Select(p => p.Name)); 

            if (PlayerParameters.Count > 0 && TagTypeParameters.Count > 0)
                text += ", ";
            
            text += string.Join(", ", TagTypeParameters); 

            text += "]";
        }
        return text;
    }
}