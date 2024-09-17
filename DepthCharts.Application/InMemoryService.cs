using AutoMapper;
using DepthCharts.Core;
using DepthCharts.Core.Entities;
using Models = DepthCharts.Core.Models;

namespace DepthCharts.Application
{
    public class InMemoryService : IDepthChartsService
    {
        private readonly IMapper _mapper;
        private readonly League _league;

        public InMemoryService(IMapper mapper, League league)
        {
            _mapper = mapper;
            _league = league;

        }

        public void AddPlayerToDepthChart(string teamName, string positionName, Models.Player player, int? position_depth)
        {
            _league.GetTeam(teamName).GetPosition(positionName).AddPlayer(_mapper.Map<Player>(player), position_depth);
        }

        public List<Models.Player> GetBackups(string teamName, string positionName, Models.Player player)
        {
            var backUpPlayers = _league.GetTeam(teamName).GetPosition(positionName).GetBackups(_mapper.Map<Player>(player));
            return _mapper.Map<List<Models.Player>>(backUpPlayers);
        }

        public Models.League GetFullDepthChart()
        {
            return _mapper.Map<Models.League>(_league);
        }

        public Models.Player RemovePlayerToDepthChart(string teamName, string positionName, Models.Player player)
        {
            var removedPlayer = _league.GetTeam(teamName).GetPosition(positionName).RemovePlayer(_mapper.Map<Player>(player));
            return _mapper.Map<Models.Player>(removedPlayer);
        }
    }
}
