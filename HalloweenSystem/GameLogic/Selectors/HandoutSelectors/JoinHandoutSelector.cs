using System.Xml;
using HalloweenSystem.GameLogic.GameObjects;
using HalloweenSystem.GameLogic.Parsing;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.HandoutSelectors;

public class JoinHandoutSelector(string separator, string placeholder, ListSelector<Handout> nestedHandouts) : ISelector<Handout>, IParser<JoinHandoutSelector>
{
	
	
	public IEnumerable<Handout> Evaluate(Context context)
	{
		var handouts = nestedHandouts.Evaluate(context).ToList();
		var text = placeholder;
		if(handouts.Count > 0)
			text = string.Join(separator, handouts.Select(handout => handout.ToHandoutText()));
		return [new Handout(text)];
	}

	public static JoinHandoutSelector Parse(XmlNode node)
	{
		var separator = node.Attributes?["separator"]?.Value ?? "";
		var placeholder = node.Attributes?["placeholder"]?.Value ?? "";
		
		if (node.HasChildNodes == false) throw new XmlException("Expected nested selectors.");

		var nestedSelectors = node.ChildNodes;
		
		var selectors = (from XmlNode nestedSelector in nestedSelectors select Parser.ParseSelector<Handout>(nestedSelector)).ToList();

		var list = new ListSelector<Handout>(selectors);
		
		return new JoinHandoutSelector(separator, placeholder, list);
	}
}