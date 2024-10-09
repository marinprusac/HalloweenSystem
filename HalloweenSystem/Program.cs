using HalloweenSystem.GameLogic;
using HalloweenSystem.GameLogic.PlayerSelectors;
using HalloweenSystem.GameLogic.RuleActions;
using HalloweenSystem.GameLogic.RuleSelectors;
using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.TagSelectors;
using HalloweenSystem.GameLogic.Utilities;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem;

class Program
{
	
	static void Main(string[] args)
	{
		
		// game tags
		var mafiaTag = new TagType("Mafia");
		var roleGroup = new TagGroup("Roles", new List<TagType> {mafiaTag});
		
		var visitedTag = new TagType("Visited");
		
		var tavernTag = new TagType("Tavern");
		var forestTag = new TagType("Forest");
		var castleTag = new TagType("Castle");
		
		var placeGroup = new TagGroup("Places", new List<TagType> {tavernTag, forestTag, castleTag});
		
		
		var m = new Player("Marin");
		var j = new Player("Juraj");
		var p = new Player("Patrik");
		var b = new Player("Borna");
		var v = new Player("Viktor");
		var n = new Player("Nika");
		var l = new Player("Lucija");
		var s = new Player("Silvija");
		
		// game data
		var setting = new Setting([roleGroup, placeGroup]);
		var context = new Context([m, j, p, b, v, n, l, s]);
		

		// rules
		var mafiaMembers = new Rule(
			"assign random mafia members", 
			[
			new AssignAction(
				new RandomPlayerSelector(new Limit<Player>("25%"),
					new EveryPlayerSelector()),
				new AssignedTagSelector(mafiaTag))
			]);

		var mafiaPlace = new Rule(
			"mafia place",
			[
				new AssignAction(true,
					new TagPlayerSelector(
						mafiaTag
						),
					new AssignedTagSelector(visitedTag,
						null,
						new RandomTagSelector(new Limit<AssignedTag>("1"),
							new GroupTagSelector(placeGroup,
								new EveryTagSelector(setting)
								)))
					)
			]);
		




		
		
		mafiaMembers.Evaluate(context);
		mafiaPlace.Evaluate(context);
		
		foreach (var player in context.Players)
		{
			Console.WriteLine(player.ToString());
		}

	}
}