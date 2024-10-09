using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLoop;

public class AssignedTag(TagType type, IEnumerable<Player>? players = null, IEnumerable<TagType>? tags = null)
{

	public TagType Type { get; } = type;
	public HashSet<Player> PlayerParameters { get; } = (players ?? []).ToHashSet();
	public HashSet<TagType> TagTypeParameters { get; } = (tags ?? []).ToHashSet();
	
	public void Update(AssignedTag tag)
	{
		if(tag.Type != Type)
			throw new ArgumentException("Cannot update tag with different type");
		AppendPlayers(tag.PlayerParameters);
		AppendTagTypes(tag.TagTypeParameters);
	}

	private void AppendPlayers(IEnumerable<Player> players)
	{
		PlayerParameters.UnionWith(players);
	}
	private void AppendTagTypes(IEnumerable<TagType> tags)
	{
		TagTypeParameters.UnionWith(tags);
	}
	public bool Covers(AssignedTag tag)
	{
		return Type ==
		       tag.Type
		       && PlayerParameters.IsSupersetOf(tag.PlayerParameters)
		       && TagTypeParameters.IsSupersetOf(tag.TagTypeParameters);
	}
	

	public AssignedTag Copy()
	{
		return new AssignedTag(Type, PlayerParameters, TagTypeParameters);
	}

	
	public new string ToString()
	{
		var text = Type.Name;
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
			text = TagTypeParameters.Aggregate(text, (current, tag) => current + (tag.Name + ", "));
			text = text[..^2];
			text += "]";
		}
		if(PlayerParameters.Count > 0 || TagTypeParameters.Count > 0)
			text += " }";
		return text;
	}
}