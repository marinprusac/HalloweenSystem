using System.Xml;

namespace HalloweenSystem.GameLogic.Utilities;

public interface IParser<out T>
{
	public static abstract T Parse(XmlNode node);
}