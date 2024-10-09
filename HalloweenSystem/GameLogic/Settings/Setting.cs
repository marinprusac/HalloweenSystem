namespace HalloweenSystem.GameLogic.Settings;

public record Setting(IEnumerable<string> Tags, IEnumerable<Rule> Rules, IEnumerable<TagGroup> TagGroups);