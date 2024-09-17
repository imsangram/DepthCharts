using Ardalis.GuardClauses;
using DepthCharts.Core.Exceptions;


namespace DepthCharts.Core.Entities;
public class Team
{
    private Team(string name, List<Position> positions)
    {
        Name = Guard.Against.NullOrEmpty(name, nameof(name));
        Positions = positions;
    }

    public string Name { get; protected set; }
    public List<Position> Positions { get; protected set; }

    public static Team Create(string name, List<Position> positions)
    {
        return new Team(name, positions);
    }

    public Position AddPosition(string positionName, List<Player> players)
    {
        // Making sure that position name stay unique. For ex. one team can have only  one position "QB"
        if (Positions.Select(x => x.Name).Contains(positionName))
        {
            throw new EntityAlreadyExistsException(nameof(Position), positionName);
        }
        var newPosition = Position.Create(positionName, players);
        Positions.Add(newPosition);
        return newPosition;
    }

    public Position GetPosition(string positionName)
    {
        var position = Positions.FirstOrDefault(x => x.Name == positionName);
        if (position == null)
        {
            throw new EntityNotFoundException(nameof(Position), positionName);
        }
        return position;
    }
}
