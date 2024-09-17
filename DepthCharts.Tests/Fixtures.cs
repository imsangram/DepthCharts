using AutoMapper;
using DepthCharts.Core.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace DepthCharts.Tests
{
    internal class Fixtures
    {
        public static IMapper GetMapper()
        {
            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(DepthChartMappingProfile));

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<IMapper>();
        }
    }
}
