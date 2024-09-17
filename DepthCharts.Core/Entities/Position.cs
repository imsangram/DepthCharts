using Ardalis.GuardClauses;
using DepthCharts.Core.Exceptions;

namespace DepthCharts.Core.Entities;

public class Position
{
    private Position(string name, List<Player> players)
    {
        Name = Name = Guard.Against.NullOrEmpty(name, nameof(name)); ;
        Players = players;
    }

    public string Name { get; protected set; }
    public List<Player> Players { get; protected set; }

    public static Position Create(string name, List<Player> players)
    {
        return new Position(name, players);
    }

    public Player AddPlayer(Player ply, int? position)
    {
        if (Players.Select(x => x.Number).Contains(ply.Number))
        {
            throw new EntityAlreadyExistsException(nameof(Player), ply.Number);
        }
        if (position >= 0)
        {
            // If position depth is provided,use that 
            Players.Insert(position.Value, ply);
        }
        else
        {
            // else push the players at the end of position
            Players.Add(ply);
        }
        return ply;
    }

    public Player RemovePlayer(Player player)
    {
        Guard.Against.NegativeOrZero(player.Number, nameof(player.Number));

        if (!Players.Select(x => x.Number).Contains(player.Number))
        {
            throw new EntityNotFoundException(nameof(Player), player.Number);
        }
        // Find the player and remove it
        var playerDepth = Players.FindIndex(m => m.Number == player.Number);
        Players.RemoveAt(playerDepth);
        return player;
    }

    public List<Player> GetBackups(Player player)
    {
        if (Players == null || Players.Count == 0)
        {
            return new List<Player>();
        }

        var currentPlayerDepth = Players.FindIndex(m => m.Number == player.Number);

        if (currentPlayerDepth < 0)
        {
            return new List<Player>();
        }

        return Players.SkipWhile(x => x.Number != player.Number).Skip(1).ToList();
    }
}

