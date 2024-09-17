using Ardalis.GuardClauses;
using DepthCharts.Core.Exceptions;

namespace DepthCharts.Core.Entities;

public class League
{
    public string Name { get; protected set; }
    public List<Team> Teams { get; protected set; }
    private League(string name, List<Team> teams = null)
    {
        Name = Guard.Against.NullOrEmpty(name, nameof(name));
        Teams = teams ?? new List<Team>();
    }
    public static League Create(string name, List<Team> teams = null)
    {
        return new League(name, teams);
    }

    public Team AddTeam(string teamName, List<Position>? positions)
    {
        if (Teams.Select(x => x.Name).Contains(teamName))
        {
            throw new EntityAlreadyExistsException(nameof(Team), teamName);
        }

        var newTeam = Team.Create(teamName, positions);
        Teams.Add(newTeam);
        return newTeam;
    }

    public Team GetTeam(string teamName)
    {
        var team = Teams.FirstOrDefault(x => x.Name == teamName);
        if (team == null)
        {
            throw new EntityNotFoundException(nameof(Team), teamName);
        }
        return team;
    }
}