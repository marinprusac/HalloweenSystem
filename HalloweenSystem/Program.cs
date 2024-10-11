using System;
using HalloweenSystem.GameLogic.GameObjects;
using HalloweenSystem.GameLogic.RuleActions;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Selectors.HandoutSelectors;
using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Selectors.TagSelectors;
using HalloweenSystem.GameLogic.Settings;


Setting setting = new(
	[
		new TagGroup("Roles", ["Human", "Mafia"]),
		new TagGroup("Relationships", ["Lover", "Rival"]),
		new TagGroup("Places", ["Tavern", "Gardens", "City Outskirts", "Sewers", "Castle", "Market"]),
	],
	[
		new Rule(
			"AssignMafias",
			[
				new AssignAction(
					new RandomSelector<Player>("25%"),
					new UnionSelector<Tag>([
							new TagSelector("Mafia"),
							new TagSelector("Evil")
						]
					)
				)
			]),

		new Rule(
			"AssignHumans",
			[
				new AssignAction(
					new ComplementSelector<Player>(
						new HasTagTypePlayerSelector("Mafia")),
					new TagSelector("Human"))
			]),

		new Rule("AssignMafiaPlace",
		[
			new AssignAction(
				true,
				new HasTagTypePlayerSelector("Mafia"),
				new TagSelector("MafiaPlace",
					null,
					new RandomSelector<Tag>("1",
						new GroupTagSelector("Places")
					))),
			new AssignAction(
				new HasTagTypePlayerSelector("MafiaPlace"),
				new TagSelector("Visited", null,
					new FromPlayerExtractTagSelector(
						"MafiaPlace",
						new HasTagTypePlayerSelector("MafiaPlace")
					)
				)
			)
		]),

		new Rule("HandoutSeenPlayers",
			[
				new HandoutAction(
					new HasTagTypePlayerSelector("Visited"),
					new ListSelector<Handout>(
						new TextHandoutSelector("You've seen some players before on the following destinations:"),
						new IteratingSelector<Tag, Handout>(
							"place",
							new FromPlayerExtractTagSelector(
								"Visited",
								new CurrentPlayerSelector()
							),
							new ListSelector<Handout>(
								new TransformHandoutSelector<Tag>(";", "no places",
									new ParameterSelector<Tag>("place")
								),
								new TextHandoutSelector(": "),
								new TransformHandoutSelector<Player>("+", "no one",
									new ChanceSelector<Player>(0.5f,
										new RemoveCurrentPlayerSelector(
											new HasTagsPlayerSelector(
												new TagSelector("Visited", null,
													new ParameterSelector<Tag>("place")
												)
											)
										)
									)
								)
							)
						)
					)
				)
			]
		)
	],
	[
		"AssignMafias",
		"AssignHumans",
		"AssignMafiaPlace",
		"AssignRandomPlaces",
		"HandoutSeenPlayers"
	]
);

var context = setting.Run(["Marin", "Jelena", "Patrik", "Borna", "Viktor", "Michelle", "Mia", "Lucija"]);
Console.WriteLine(context.ToString());