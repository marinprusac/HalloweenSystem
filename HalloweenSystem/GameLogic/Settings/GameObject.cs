namespace HalloweenSystem.GameLogic.Settings;

public class GameObject(string name = "") : IEquatable<GameObject>
{

	public string Name { get; } = name;

	

	public static IEnumerable<GameObject> Union<T>(IEnumerable<IEnumerable<GameObject>> objectSets) where T : GameObject, new()
	{
		var obj = new T();
		return obj._union(objectSets);
	}
	
	public static IEnumerable<GameObject> Intersect<T>(IEnumerable<IEnumerable<GameObject>> objectSets) where T : GameObject, new()
	{
		var obj = new T();
		return obj._intersect(objectSets);
	}
	
	public static IEnumerable<GameObject> Everything<T>(Context context) where T : GameObject, new()
	{
		var obj = new T();
		return obj._everything(context);
	}
	
	public static IEnumerable<GameObject> Complement<T>(IEnumerable<GameObject> objectSet, Context context) where T : GameObject, new()
	{
		var obj = new T();
		return obj._complement(objectSet, context);
	}
	
	protected virtual IEnumerable<GameObject> _everything(Context context)
	{
		throw new Exception("Everything is not defined for this object");
	}

	protected virtual IEnumerable<GameObject> _complement(IEnumerable<GameObject> objectSet, Context context)
	{
		throw new Exception("Complement is not defined for this object");
	}


	
	protected virtual IEnumerable<GameObject> _union(IEnumerable<IEnumerable<GameObject>> objectSets)
	{
		var objectSetsList = objectSets.ToList();
		var result = new List<GameObject>();
		foreach (var objectSet in objectSetsList)
		{
			foreach (var gameObject in objectSet)
			{
				if(result.Any(g => g.Equals(gameObject))) continue;
				result.Add(gameObject);
			}
		}

		return result;
	}

	protected virtual IEnumerable<GameObject> _intersect(IEnumerable<IEnumerable<GameObject>> objectSets)
	{
		var objectSetsList = objectSets.ToList();
		
		if(objectSetsList.Count == 0) throw new ArgumentException("Cannot intersect empty list of object sets");
		
		var result = objectSetsList[0].ToList();

		foreach (var obj in result.ToList())
		{
			foreach (var objectSet in objectSetsList)
			{
				if(objectSet.Any(o => o.Equals(obj))) continue;
				result.Remove(obj);
			}
			
		}

		return result;
	}


	public bool Equals(GameObject? other)
	{
		if (other is null) return false;
		if (ReferenceEquals(this, other)) return true;
		return Name == other.Name;
	}

	public override bool Equals(object? obj)
	{
		if (obj is null) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
		return Equals((GameObject)obj);
	}

	public override int GetHashCode()
	{
		return Name.GetHashCode();
	}

	public static bool operator ==(GameObject? left, GameObject? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(GameObject? left, GameObject? right)
	{
		return !Equals(left, right);
	}
}