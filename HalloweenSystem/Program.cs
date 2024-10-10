using HalloweenSystem.GameLogic.HandoutParts;
using HalloweenSystem.GameLogic.RuleActions;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Selectors.TagSelectors;
using HalloweenSystem.GameLogic.Settings;




Setting setting = new(
	[
		new TagGroup("Roles", ["Human", "Vampire"]),
		new TagGroup("Relationships", ["Lover", "Rival"]),
		new TagGroup("Places", ["Tavern", "Gardens", "City Outskirts", "Sewers", "Castle", "Market"]),
	],
	[
		new Rule(
			"AssignVampires",
			[
				new AssignAction(
					new RandomSelector<Player>("25%",
						new EverySelector<Player>()),
					new TagSelector("Vampire"))
			]),
		new Rule(
			"AssignHumans",
			[
				new AssignAction(
					new NotSelector<Player>(
						new HasTagTypePlayerSelector("Vampire",
							new EverySelector<Player>()
						)),
					new TagSelector("Human"))
			]),
		new Rule("AssignVampirePlace",
		[
			new AssignAction(true,
				new HasTagTypePlayerSelector("Vampire"),
				new TagSelector("VampirePlace", null,
					new RandomSelector<Tag>("1",
						new GroupTagSelector("Places")
					))),
			new AssignAction(
				new HasTagTypePlayerSelector("VampirePlace"),
				
				new TagSelector("Visited", null,
					
					new FromPlayerExtractTagSelector(
						"VampirePlace",
						new HasTagTypePlayerSelector("VampirePlace")
						)
					)
				),
			new AssignAction(false,
				new HasTagTypePlayerSelector("Vampire"),
				
				new TagSelector("Visited", null,
					new RandomSelector<Tag>("1",
						new AllSelector<Tag>([
							new GroupTagSelector("Places"),
							new NotSelector<Tag>(
								new FromPlayerExtractTagSelector(
									"Visited",
									new CurrentPlayerSelector()
								))
							])
						)
					)
				)
		]),
		new Rule("AssignRandomPlaces",
			[
				new AssignAction(false,
					new HasTagTypePlayerSelector("Human"),
					new TagSelector("Visited", null,
						new RandomSelector<Tag>("2",
							new GroupTagSelector("Places")
						)))
			]
		),
		new Rule("HandoutSeenPlayers",
			[
				new HandoutAction(
					new HasTagTypePlayerSelector("Visited"),
					
					new ListHandoutPart([
						
						new TextHandoutPart("\nYou've seen some players before on the following destinations:\n"),
						
						new IterableHandoutPart<Tag>("\n", "place",

							new FromPlayerExtractTagSelector(
								"Visited",
								new CurrentPlayerSelector()
							),
							
							new ListHandoutPart([
									new TextHandoutPart("\t"),
									new FillHandoutPart("place"),
									new TextHandoutPart(": "),
									new IterableHandoutPart<Player>(", ", "player",
										new RandomSelector<Player>("50%",

										new RemoveCurrentPlayerSelector(
											new HasTagsPlayerSelector(
												new TagSelector("Visited", null,
													new FillSelector<Tag>("place")
													)
												)
											)
										),
										new FillHandoutPart("player")
									)
								]
								))
					])
					)
			]),
	],
	[
		"AssignVampires",
		"AssignHumans",
		"AssignVampirePlace",
		"AssignRandomPlaces",
		"HandoutSeenPlayers"
	]
);

var context = setting.Run(["Marin", "Jelena", "Patrik", "Borna", "Viktor", "Michelle", "Mia", "Lucija"]);
Console.WriteLine(context.ToString());
