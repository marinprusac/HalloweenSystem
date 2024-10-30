using System.Xml;
using HalloweenSystem.GameLogic.GameObjects;
using HalloweenSystem.GameLogic.Parsing;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.HandoutSelectors;

public class JoinHandoutSelector(string separator, string placeholder, ISelector<Handout> nestedHandouts) : ISelector<Handout>, IParser<JoinHandoutSelector>
{
	
	
	public IEnumerable<Handout> Evaluate(Context context)
	{
		var handouts = nestedHandouts.Evaluate(context).ToList();
		var text = placeholder;
		if (handouts.Count <= 0) return [new Handout(text)];
		text = separator == "newline" ? string.Join("\n", handouts.Select(handout => handout.ToHandoutText())) : string.Join(separator, handouts.Select(handout => handout.ToHandoutText()));
		return [new Handout(text)];
	}

	public static JoinHandoutSelector Parse(XmlNode node)
	{
		var separator = node.Attributes?["separator"]?.Value ?? "";
		
		var placeholder = node.Attributes?["placeholder"]?.Value ?? "";
		
		var list = ListSelector<Handout>.Parse(node);
		
		return new JoinHandoutSelector(separator, placeholder, list);
	}
}