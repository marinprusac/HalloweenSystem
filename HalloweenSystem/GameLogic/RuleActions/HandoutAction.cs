using HalloweenSystem.GameLogic.HandoutParts;
using HalloweenSystem.GameLogic.PlayerSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.RuleActions;

public class HandoutAction : RuleAction
{

	public HandoutAction(PlayerSelector playerSelector, List<HandoutPart> handoutParts)
	{
		_playerSelector = playerSelector;
		_handoutParts = handoutParts;
	}
	
	public HandoutAction(PlayerSelector playerSelector, TagType perTagTypeContent, List<HandoutPart> handoutParts)
	{
		_playerSelector = playerSelector;
		_perTagTypeContent = perTagTypeContent;
		_handoutParts = handoutParts;
	}
	
	private PlayerSelector _playerSelector;
	private TagType? _perTagTypeContent;
	private List<HandoutPart> _handoutParts;
	
	private void HandoutToPlayer(Player playerToReceive, Player? iteratedPlayer=null, TagType? iteratedTagType=null)
	{
		foreach (var handoutPart in _handoutParts)
		{
			
		}
	}
	
	
	public override void Evaluate(Context context)
	{
		var players = _playerSelector.Evaluate(context);
		foreach (var player in players)
		{
			if (_perTagTypeContent == null)
			{
				HandoutToPlayer(player);
				continue;
			}

			if (!player.GetTagOfType((TagType)_perTagTypeContent, out AssignedTag? tag)) continue;

			if(tag == null) continue;

			if (tag.PlayerParameters.Count > 0)
			{
				foreach (var playerParameter in tag.PlayerParameters)
				{
					if (tag.TagTypeParameters.Count > 0)
					{
						foreach (var tagTypeParameter in tag.TagTypeParameters)
						{
							HandoutToPlayer(player, playerParameter, tagTypeParameter);
						}
						continue;
					}
					HandoutToPlayer(player, playerParameter, null);
				}
			}
			else
			{
				foreach (var tagTypeParameter in tag.TagTypeParameters)
				{
					HandoutToPlayer(player, null, tagTypeParameter);
				}
			}
		}
		
		
		
	}
}