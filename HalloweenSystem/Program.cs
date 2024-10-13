using HalloweenSystem.GameLogic.Parsing;


const string path = "/home/marin/Development/HalloweenSystem/HalloweenSystem/example";

var s = Parser.LoadGame(path);

var ctx = s.RunWithTags(new Dictionary<string, List<string>>()
{
	{"Marin", ["Wanderer"]},
	{"Patrik", ["Guard"]},
	{"Juraj", ["Wanderer"]},
	{"Borna", ["Social"]},
	{"Jelena", ["Social"]},
	{"Lucija", ["Social"]},
	{"Silvija", ["Guard"]},
	{"Mauro", ["Social"]},
	{"Luka", ["Social"]},
	{"Nika", ["Royal"]},
});

Console.WriteLine(ctx.ToString());

