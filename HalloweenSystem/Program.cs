using HalloweenSystem.GameLogic;
using HalloweenSystem.GameLogic.HandoutParts;
using HalloweenSystem.GameLogic.RuleActions;
using HalloweenSystem.GameLogic.RuleSelectors;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Selectors.TagSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;
using HalloweenSystem.GameLogic.Utilities.Iterators;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem;

class Program
{
	private static readonly Player Marin = new("Marin");
	private static readonly Player Patrik = new("Patrik");
	private static readonly Player Borna = new("Borna");
	private static readonly Player Juraj = new("Juraj");
	private static readonly Player Jelena = new("Jelena");
	private static readonly Player Viktor = new("Viktor");
	private static readonly Player Mia = new("Mia");
	

	private static readonly TagGroup Locations = new TagGroup("Locations", ["Tavern", "Forest", "Castle"]);
	
	private static readonly Setting Setting = new(Locations.Tags,[],[Locations]);
	private static readonly Context Context = new(Setting, [Marin, Patrik, Borna, Juraj, Jelena, Viktor, Mia]);
	
	

	
	
	static void Main(string[] args)
	{
		

		
		




	}
}