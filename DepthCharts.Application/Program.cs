using DepthCharts.Application;
using DepthCharts.Core;
using DepthCharts.Core.Profiles;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddAutoMapper(typeof(DepthChartMappingProfile));
services.AddTransient<IDepthChartsService, InMemoryService>();

var data = Utils.SeedData();
services.AddSingleton(data);
var serviceProvider = services.BuildServiceProvider();

// Resolve an instance of IDepthChartsService (which is InMemoryService)
var depthChartsService = serviceProvider.GetRequiredService<IDepthChartsService>();

var fullChart = depthChartsService.GetFullDepthChart();

// Print the full chart
Console.WriteLine($" ------ League: {fullChart.Name} --------");
Console.WriteLine($" ----------------------------------------");
foreach (var team in fullChart.Teams)
{

    Console.WriteLine($" ------ Team: {team.Name} --------");
    Console.WriteLine($" ----------------------------------------");
    foreach (var position in team.Positions)
    {
        Console.WriteLine($" ------ Position: {position.Name} --------  {string.Join(",", position.Players.Select(x => $"# {x.Number} {x.Name}"))}");
    }
}

Console.ReadLine();