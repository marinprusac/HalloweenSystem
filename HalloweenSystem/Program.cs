using HalloweenSystem.GameLogic.Parsing;


const string path = "/home/marin/Development/HalloweenSystem/HalloweenSystem/example";

var s = Parser.LoadGame(path);

var ctx = s.RunWithTags(new Dictionary<string, List<string>>()
{
	{"Marin", ["Commoner", "Soldier", "Old"]},
	{"Patrik", ["Commoner", "Loner", "Guard", "Young"]},
	{"Juraj", ["Aristocrat", "Ambassador"]},
	{"Borna", ["Aristocrat", "Social", "Young"]},
	{"Viktor", ["Commoner", "Social"]},
	{"Jelena", ["Aristocrat", "Social", "Inheritor"]},
	{"Tin", ["Aristocrat", "Court mage"]},
	{"Lucija", ["Commoner", "Soldier", "Social"]},
	{"Silvija", ["Commoner", "Guard"]},
	{"Rino", ["Aristocrat", "Army general", "Old"]},
	{"Luka", ["Commoner", "Loner"]},
	{"Nika", ["Aristocrat", "Queen"]},
	{"Michelle", ["Commoner", "Librarian", "Old"]},
	{"Mathea", ["Commoner", "Bard"]},
	{"Mia", ["Aristocrat", "Governor", "Young"]},
});

Console.WriteLine(ctx.ToString());

