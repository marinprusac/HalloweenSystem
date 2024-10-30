using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Utilities;

public class Constrainer
{

	private Constrainer()
	{
	}
	
	private List<Func<Context, float>> _graders = [];
	
	public static Constrainer Build()
	{
		return new Constrainer();
	}
	

	public Constrainer AddConstraint(Func<Context, float> gradeFunction)
	{
		_graders.Add(gradeFunction);
		return this;
	}
	
	public float Evaluate(Context context)
	{
		float grade = 1;
		foreach (var gradeFunction in _graders)
		{
			grade *= gradeFunction(context);
		}

		return grade;
	}

	public Context ForceGrade(Setting setting, Dictionary<string, List<string>> players, float minimum)
	{
		float grade;
		Context ctx;

		do
		{
			ctx = setting.RunWithTags(players);
			grade = Evaluate(ctx);

		} while (grade < minimum);

		return ctx;
	}



	public static readonly Func<Context, float> ContentDistributionGrade = (context) =>
	{
		var counts = context.Players.Where(p => p.Name != "Nika").Select(p => p.Handouts.Count).ToList();
		var min = counts.Min();
		var max = counts.Max();
		return 1 - (max - min) / (float)max;
	};


}