using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.TagSelectors;

public class ListTagSelector(IEnumerable<AssignedTagSelector> nestedTagSelectors) : TagSelector
{
	public override IEnumerable<AssignedTag> Evaluate(Context context, Player? operatedPlayer = null)
	{
		List<AssignedTag> tags = [];
		foreach (var nestedTagSelector in nestedTagSelectors)
		{
			foreach (var newTag in nestedTagSelector.Evaluate(context))
			{
				var duplicateTag = tags.FirstOrDefault(t => t.Type == newTag.Type);
				if (duplicateTag != null)
				{
					duplicateTag.Update(newTag);
				}
				else
				{
					tags.Add(newTag);
				}
			}
		}

		return tags;
	}
}