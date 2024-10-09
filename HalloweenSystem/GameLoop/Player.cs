using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLoop;

public class Player(string name)
{
	
	public Player(string name, IEnumerable<AssignedTag> assignedTags) : this(name)
	{
		AssignedTags = assignedTags.ToList();
	}

	private List<AssignedTag> AssignedTags { get; } = [];
	public string Name { get; } = name;

	
	public bool GetTagOfType(TagType type, out AssignedTag? tag)
	{
		tag = AssignedTags.FirstOrDefault(tag => tag.Type == type);
		return tag != null;
	}
	
	public bool GetQueriedTag(AssignedTag queryTag, out AssignedTag? tag)
	{
		tag = AssignedTags.FirstOrDefault(tag => tag.Covers(queryTag));
		return tag != null;
	}

	
	public IEnumerable<Player> ExtractPlayers(TagType tagType)
	{
		return AssignedTags.Where(t => t.Type == tagType).SelectMany(t => t.PlayerParameters);
	}

	public void AssignTags(IEnumerable<AssignedTag> newTags)
	{
		foreach (var newTag in newTags)
		{
			if (GetTagOfType(newTag.Type, out var assignedTag))
			{
				assignedTag?.Update(newTag);
			}
			else
			{
				AssignedTags.Add(newTag.Copy());
			}
		}
	}

	
	public new string ToString()
	{
		var text = "";
		text += Name + " { ";
		text = AssignedTags.Aggregate(text, (current, tag) => current + (tag.ToString() + ", "));
		if(AssignedTags.Count > 0)
			text = text[..^2];
		text += " }";
		return text;
	}


}