using Ardalis.GuardClauses;

namespace DepthCharts.Core.Entities;

public class Player
{
    public int Number { get; set; }
    public string Name { get; set; }

    private Player(int number, string name)
    {
        Number = number;
        Name = Guard.Against.NullOrEmpty(name, nameof(name)); ;
    }

    public static Player Create(int number, string name)
    {
        return new Player(number, name);
    }
}


