using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLoop;

public class Player(string name) : GameObject(name)
{
	
	public Player(string name, IEnumerable<Tag> assignedTags) : this(name)
	{
		AssignedTags = assignedTags.ToList();
	}

	public Player() : this("")
	{
		
	}


	public List<string> Handouts { get; } = [];
	public List<Tag> AssignedTags { get; } = [];
	

	
	public bool GetTagOfType(string type, out Tag? tag)
	{
		tag = AssignedTags.FirstOrDefault(tag => tag.Name == type);
		return tag != null;
	}
	
	public bool GetQueriedTag(Tag queryTag, out Tag? tag)
	{
		tag = AssignedTags.FirstOrDefault(tag => tag.Covers(queryTag));
		return tag != null;
	}
	

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

	
	public new string ToString()
	{
		var text = "";
		text += Name + " { ";
		text += "Tags: [ ";
		text = AssignedTags.Aggregate(text, (current, tag) => current + (tag.ToString() + ", "));
		if(AssignedTags.Count > 0)
			text = text[..^2];
		text += " ]";
		
		text += " Handouts: [ ";
		text = Handouts.Aggregate(text, (current, handout) => current + (handout + ", "));
		if(Handouts.Count > 0)
			text = text[..^2];
		text += " ]";
		
		text += " }";
		return text;
	}


	protected override IEnumerable<GameObject> _everything(Context context)
	{
		return context.Players;
	}

	protected override IEnumerable<GameObject> _complement(IEnumerable<GameObject> objectSet, Context context)
	{
		return context.Players.Except(objectSet);
	}
}