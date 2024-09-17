
namespace DepthCharts.Core.Models
{
    public record League(string Name, List<Team> Teams);
    public record Team(string Name, List<Position> Positions);
    public record Position(string Name, List<Player> Players);
    public record Player(int Number, string Name);
}
