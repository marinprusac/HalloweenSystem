using System;
using System.Collections.Generic;
using System.Linq;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Utilities;

/// <summary>
/// Represents a utility class that limits the number of objects of type T based on a specified amount.
/// </summary>
/// <typeparam name="T">The type of the objects to be limited, which must be a GameObject.</typeparam>
/// <param name="amount">The amount to limit the objects by, either as a percentage or a fixed number.</param>
public class Limit<T>(string amount) where T : GameObject
{
    /// <summary>
    /// Evaluates the given collection of objects and returns a limited collection based on the specified amount.
    /// </summary>
    /// <param name="objects">The collection of objects to be limited.</param>
    /// <returns>A limited collection of objects of type T.</returns>
    /// <exception cref="ArgumentException">Thrown when the amount is not a valid percentage or number.</exception>
    public IEnumerable<T> Evaluate(IEnumerable<T> objects)
    {
        var enumerable = objects.ToList();
        var count = enumerable.Count;
        int selectedCount;
        if (amount.Contains('%'))
        {
            var success = int.TryParse(amount.Replace("%", ""), out var result);
            if(success) selectedCount = (int)Math.Ceiling(count * (result / 100f));
            else throw new ArgumentException();
        }
        else
        {
            var success = int.TryParse(amount, out var result);
            if (success) selectedCount = result;
            else throw new ArgumentException();
        }


        if (typeof(T) == typeof(Player))
            return EvaluateForPlayers(enumerable.Cast<Player>().ToList(), selectedCount).Cast<T>();

        var random = new Random();
        var selected = enumerable.ToList();
        for(var i = 0; i < count - selectedCount; i++)
        {
            if(selected.Count == 0) break;
            selected.RemoveAt(random.Next(selected.Count));
        }

        return selected;
    }


    private IEnumerable<Player> EvaluateForPlayers(List<Player> players, int selectedCount)
    {
        
        if(players.Count <= selectedCount) return players;
        
        
        var maxOccupied = players.Max(p => p.AssignedTags.Count) + 1;
        var totalOccupation = players.Sum(p => maxOccupied - p.AssignedTags.Count);
        var random = new Random();

        var leftOverOccupation = totalOccupation;
        var possiblePlayers = players.ToList();
        
        var selectedPlayers = new List<Player>();
        
        for (var i = 0; i < selectedCount; i++)
        {
            var randomTagPlayer = random.Next(leftOverOccupation);
            Player? chosenPlayer = null;

            foreach (var player in possiblePlayers)
            {
                if (randomTagPlayer == 0)
                {
                    chosenPlayer = player;
                    break;
                }
                randomTagPlayer -= maxOccupied - player.AssignedTags.Count;
                if (randomTagPlayer >= 0) continue;
                chosenPlayer = player;
                break;
            }
            
            if(chosenPlayer == null) throw new Exception("Player not found");
            selectedPlayers.Add(chosenPlayer);
            leftOverOccupation -= maxOccupied - chosenPlayer.AssignedTags.Count;
            possiblePlayers.Remove(chosenPlayer);


        }

        return selectedPlayers;
    }
    
    private IEnumerable<Player> EvaluateForPlayersV2(List<Player> players, int selectedCount)
    {
        
        if(players.Count <= selectedCount) return players;
        var playerArray = players.ToArray();
        var random = new Random();
        random.Shuffle(playerArray);
        var playerList = playerArray.ToList();
        playerList.Sort((p1, p2) => p1.AssignedTags.Count <= p2.AssignedTags.Count ? -1 : 1);
        return playerList.GetRange(0, selectedCount);
    }
}