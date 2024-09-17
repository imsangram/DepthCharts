using AutoMapper;
using DepthCharts.Core.Entities;

namespace DepthCharts.Core.Profiles
{
    public class DepthChartMappingProfile : Profile
    {
        public DepthChartMappingProfile()
        {
            CreateMap<League, Models.League>();
            CreateMap<Player, Models.Player>().ReverseMap();
            CreateMap<Position, Models.Position>();
            CreateMap<Team, Models.Team>();
        }
    }
}
