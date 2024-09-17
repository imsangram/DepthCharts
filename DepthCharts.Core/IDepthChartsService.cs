using DepthCharts.Core.Entities;


namespace DepthCharts.Core
{
    public interface IDepthChartsService
    {
        void AddPlayerToDepthChart(string teamName, string positionName, Models.Player player, int? position_depth = -1);
        Models.Player RemovePlayerToDepthChart(string teamName, string positionName, Models.Player player);
        List<Models.Player> GetBackups(string teamName, string position, Models.Player player);
        Models.League GetFullDepthChart();
    }
}
