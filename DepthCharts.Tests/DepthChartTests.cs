using DepthCharts.Core.Entities;
using Models = DepthCharts.Core.Models;
using FluentAssertions;
using AutoMapper;
using DepthCharts.Core;
using DepthCharts.Application;
using DepthCharts.Core.Exceptions;

namespace DepthCharts.Tests
{
    [TestFixture]
    internal class DepthChartTests
    {
        public IDepthChartsService service { get; private set; }

        [SetUp]
        public void SetupBeforeEachTest()
        {
            // Seed data for testing
            var seededLeagueData = SeedData();
            IMapper mapper = Fixtures.GetMapper();
            service = new InMemoryService(mapper, seededLeagueData);
        }


        private League SeedData()
        {
            var league = League.Create(Constants.NFL);

            var tomBrady = Player.Create(12, "Tom Brady");
            var blainGabbert = Player.Create(11, "Blain Gabbert");
            var kyleTrask = Player.Create(80, "Kyle Trask");

            var qbPositions = Position.Create(Constants.Quarterback, [tomBrady, blainGabbert, kyleTrask]);

            var teamTempa = league.AddTeam(Constants.TempaBayBuccaneers, [qbPositions]);

            return league;

        }

        [Test]
        public void AddPlayerToDepthChart_Should_AddPlayerToCorrectPosition()
        {
            // Arrange
            var position_depth = 2;
            var johnDoe = new Models.Player(13, "John Doe");
            var maxPayne = new Models.Player(100, "Max Payne");

            // Act
            service.AddPlayerToDepthChart(Constants.TempaBayBuccaneers, Constants.Quarterback, johnDoe, position_depth);

            // Assert
            var actual = service.GetFullDepthChart();
            actual.Should().NotBeNull();

            // Assert that new player has been inserted at pos depth = 2
            actual.Should().BeEquivalentTo
                (new Models.League(Constants.NFL,
                        [new Models.Team(Constants.TempaBayBuccaneers,
                            [new Models.Position(Constants.Quarterback,
                                [new Models.Player(12, "Tom Brady"), new Models.Player(11, "Blain Gabbert"),new Models.Player(13, "John Doe"),new Models.Player(80, "Kyle Trask"),])])]));

            // When Depth is Not provided, a new player has been added at the end
            service.AddPlayerToDepthChart(Constants.TempaBayBuccaneers, Constants.Quarterback, maxPayne);
            var newFullDepth = service.GetFullDepthChart();
            var lastPlayer = newFullDepth.Teams.Find(x => x.Name == Constants.TempaBayBuccaneers)?.Positions.Find(x => x.Name == Constants.Quarterback)?.Players.Last();
            lastPlayer.Should().BeEquivalentTo(maxPayne);
        }

        [Test]
        public void AddPlayerToDepthChart_Should_ThrowError_WhenAddingDuplicatePlayer()
        {
            // Arrange
            var duplicateTomBrady = new Models.Player(12, "Tom Brady");

            // Act & assert
            var ex = Assert.Throws<EntityAlreadyExistsException>(() => service.AddPlayerToDepthChart(Constants.TempaBayBuccaneers, Constants.Quarterback, duplicateTomBrady, 2));
            ex.Message.Should().Be("Player already exists.");
        }

        [Test]
        public void GetBackups_Should_ReturnCorrectResponse()
        {
            // Arrange
            var position_depth = 2;
            var johnDoe = new Models.Player(13, "John Doe");

            // Act
            service.AddPlayerToDepthChart(Constants.TempaBayBuccaneers, Constants.Quarterback, johnDoe, position_depth);

            // Assert all backup scnenarios

            var tomBradyBackups = service.GetBackups(Constants.TempaBayBuccaneers, Constants.Quarterback, new Models.Player(12, "Tom Brady"));
            tomBradyBackups.Should().BeEquivalentTo([new Models.Player(11, "Blain Gabbert"), new Models.Player(13, "John Doe"), new Models.Player(80, "Kyle Trask")]);

            // For last player in position, backups should be empty
            var kyleTrasKBackups = service.GetBackups(Constants.TempaBayBuccaneers, Constants.Quarterback, new Models.Player(80, "Kyle Trask"));
            kyleTrasKBackups.Should().BeEmpty();

            // if a player doesn't exists then it should return empty result
            var nonExistingPlayerBackups = service.GetBackups(Constants.TempaBayBuccaneers, Constants.Quarterback, new Models.Player(Faker.RandomNumber.Next(), Faker.Name.FullName()));
            nonExistingPlayerBackups.Should().BeEmpty();
        }

        [Test]
        public void RemovePlayerFromDepthChart_Should_RemovePlayer()
        {
            // Act
            service.RemovePlayerToDepthChart(Constants.TempaBayBuccaneers, Constants.Quarterback, new Models.Player(12, "Tom Brady"));

            // Assert
            var fullDepthChart = service.GetFullDepthChart();
            var QbPlayers = fullDepthChart.Teams.Find(x => x.Name == Constants.TempaBayBuccaneers)?.Positions.Find(x => x.Name == Constants.Quarterback)?.Players;
            QbPlayers.Should().BeEquivalentTo([new Models.Player(11, "Blain Gabbert"), new Models.Player(80, "Kyle Trask")]);
        }

        [Test]
        public void RemovePlayerFromDepthChart_Should_ThrowError_WhenPlayerOrPositionDoesntExist()
        {
            // Arrange
            var nonExistentPlayer = new Models.Player(Faker.RandomNumber.Next(), Faker.Name.FullName());
            var invalidPositionName = "ABCD";

            // Act & assert
            var ex = Assert.Throws<EntityNotFoundException>(() => service.RemovePlayerToDepthChart(Constants.TempaBayBuccaneers, Constants.Quarterback, nonExistentPlayer));
            ex.Message.Should().Be($"{nameof(Player)} with key {nonExistentPlayer.Number} was not found.");

            var exc = Assert.Throws<EntityNotFoundException>(() => service.RemovePlayerToDepthChart(Constants.TempaBayBuccaneers, invalidPositionName, new Models.Player(12, "Tom Brady")));
            exc.Message.Should().Be($"{nameof(Position)} with key {invalidPositionName} was not found.");
        }
    }
}
