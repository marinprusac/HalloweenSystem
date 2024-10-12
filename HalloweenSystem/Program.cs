using HalloweenSystem.GameLogic.Parsing;


var path = "/home/marin/Development/HalloweenSystem/HalloweenSystem/example";

var s = Parser.LoadGame(path);

var ctx = s.Run(["Marin", "Jelena", "Patrik", "Borna", "Viktor", "Michelle", "Mia", "Lucija"]);

Console.WriteLine(ctx.ToString());




//var context = setting.Run(["Marin", "Jelena", "Patrik", "Borna", "Viktor", "Michelle", "Mia", "Lucija"]);
//Console.WriteLine(context.ToString());