using Const = DepthCharts.Core.Constants;
using DepthCharts.Core.Entities;

namespace DepthCharts.Application;

public static class Utils
{
    public static League SeedData()
    {
        var league = League.Create(Const.NFL);

        var tomBrady = Player.Create(12, "Tom Brady");
        var blainGabbert = Player.Create(11, "Blain Gabbert");
        var kyleTrask = Player.Create(80, "Kyle Trask");
        var jaelonDarden = Player.Create(55, "Jaelon Darden");
        var mikeEvans = Player.Create(56, "Mike Evans");

        var qbPositions = Position.Create(Const.Quarterback, [tomBrady, blainGabbert, kyleTrask]);
        var centerPositions = Position.Create(Const.Center, [kyleTrask, jaelonDarden]);
        var tePositions = Position.Create(Const.TightEnd, [mikeEvans, blainGabbert]);

        var teamTempa = league.AddTeam(Const.TempaBayBuccaneers, [qbPositions, centerPositions, tePositions]);

        return league;

    }
}