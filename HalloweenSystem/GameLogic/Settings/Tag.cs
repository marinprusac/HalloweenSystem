using System;
using System.Collections.Generic;
using System.Linq;

namespace HalloweenSystem.GameLogic.Settings;

public class Tag(string name = "", IEnumerable<Player>? players = null, IEnumerable<string>? tags = null) : GameObject(name)
{

	public Tag() : this("")
	{
		
	}
	
	public HashSet<Player> PlayerParameters { get; } = (players ?? []).ToHashSet();
	public HashSet<string> TagTypeParameters { get; } = (tags ?? []).ToHashSet();


	protected override IEnumerable<GameObject> _everything(Context context)
	{
		var tags = context.Setting.Tags ?? throw new ArgumentException("Tags are not defined in the setting");
		return tags.Select(tag => new Tag(tag));
	}
	
	protected override IEnumerable<GameObject> _complement(IEnumerable<GameObject> objectSet, Context context)
	{
		var everything = _everything(context);
		return everything.Except(objectSet);
	}

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
	
	protected override IEnumerable<GameObject> _intersect(IEnumerable<IEnumerable<GameObject>> objectSets)
	{
		
		var tagSets = objectSets.Select(set => set.Cast<Tag>()).ToList();
		
		if(tagSets.Count == 0) throw new ArgumentException("Cannot intersect empty list of object sets");
		
		var result = tagSets[0].ToList();
		var result2 = new List<Tag>();

		for (int i = 1; i < tagSets.Count; i++)
		{
			var tagSet = tagSets[i];
			foreach (var tag in tagSet)
			{
				var duplicatedTag = result.Find(t => t.Equals(tag));
				if(duplicatedTag == null) continue;
				duplicatedTag.Restrict(tag);
				result2.Add(duplicatedTag.Copy());
			}
			result = result2;
		}

		return result;
	} 
	
	public void Extend(Tag tag)
	{
		if(tag.Name != Name)
			throw new ArgumentException("Cannot update tag with different type");
		PlayerParameters.UnionWith(tag.PlayerParameters);
		TagTypeParameters.UnionWith(tag.TagTypeParameters);
	}

	public void Restrict(Tag tag)
	{
		if(tag.Name != Name)
			throw new ArgumentException("Cannot update tag with different type");
		PlayerParameters.IntersectWith(tag.PlayerParameters);
		TagTypeParameters.IntersectWith(tag.TagTypeParameters);
	}


	public bool Covers(Tag tag)
	{
		return Name ==
		       tag.Name
		       && PlayerParameters.IsSupersetOf(tag.PlayerParameters)
		       && TagTypeParameters.IsSupersetOf(tag.TagTypeParameters);
	}
	

	public Tag Copy()
	{
		return new Tag(Name, PlayerParameters, TagTypeParameters);
	}

	public static Tag operator*(Tag tag1, Tag tag2)
	{
		if(tag1.Name != tag2.Name) throw new InvalidOperationException();
		var newTag = tag1.Copy();
		newTag.Restrict(tag2);
		return newTag;
	}
	
	public static Tag operator+(Tag tag1, Tag tag2)
	{
		if (tag1.Name != tag2.Name) throw new InvalidOperationException();
		var newTag = tag1.Copy();
		newTag.Extend(tag2);
		return newTag;
	}
	
	
	
	public new string ToString()
	{
		var text = Name;
		if(PlayerParameters.Count > 0 || TagTypeParameters.Count > 0)
			text += " { ";
		if (PlayerParameters.Count > 0)
		{
			text += "Players[";
			text = PlayerParameters.Aggregate(text, (current, player) => current + (player.Name + ", "));
			text = text[..^2];
			text += "]";
			
		}
		
		if(PlayerParameters.Count > 0 && TagTypeParameters.Count > 0)
			text += ", ";

		if (TagTypeParameters.Count > 0)
		{
			text += "Tags[";
			text = TagTypeParameters.Aggregate(text, (current, name) => current + (name + ", "));
			text = text[..^2];
			text += "]";
		}
		if(PlayerParameters.Count > 0 || TagTypeParameters.Count > 0)
			text += " }";
		return text;
	}

}