using HalloweenSystem.GameLoop;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.TagSelectors;

public class EveryTagSelector(Setting setting) : TagSelector
{
	
	public override IEnumerable<AssignedTag> Evaluate(Context context, Player? operatedPlayer = null)
	{
		HashSet<TagType> allTags = setting.TagGroups.SelectMany(tg => tg.Tags).ToHashSet();
		IEnumerable<AssignedTag> tags = allTags.Select(t => new AssignedTag(t));
		return tags;
	}
}