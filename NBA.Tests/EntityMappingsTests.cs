using NBA.Api.Mappings;
using NBA.Data.Entities;
using NBA.Data.Enumerations;
using Xunit;

namespace NBA.Tests
{
    // Guards the centralized entity -> DTO mapping that replaced the copy-pasted blocks.
    // If a field is dropped during a future refactor, these fail instead of silently
    // returning nulls to the client.
    public class EntityMappingsTests
    {
        [Fact]
        public void ToLeagueDto_copies_all_scalar_fields()
        {
            var league = new League
            {
                Leagueid = 7,
                Name = "Dunk Dynasty",
                Commissioner = 3,
                Seasonyear = "2026/2027",
                Weeksforseason = 18,
                Transactionlimit = 40,
                Autostart = true,
                Typetransactionlimits = 2,
                Typeleague = 1,
                Draftstyle = 1,
                Statsvalueid = 99,
            };

            var dto = league.ToLeagueDto();

            Assert.Equal(7, dto.Leagueid);
            Assert.Equal("Dunk Dynasty", dto.Name);
            Assert.Equal(3, dto.Commissioner);
            Assert.Equal("2026/2027", dto.Seasonyear);
            Assert.Equal(18, dto.Weeksforseason);
            Assert.Equal(40, dto.Transactionlimit);
            Assert.True(dto.Autostart);
            Assert.Equal(2, dto.Typetransactionlimits);
            Assert.Equal(1, dto.Typeleague);
            Assert.Equal(1, dto.Draftstyle);
            Assert.Equal(99, dto.Statsvalueid);
            Assert.Null(dto.CommissionersTeam);
        }

        [Fact]
        public void ToTeamDto_copies_all_scalar_fields()
        {
            var team = new Team
            {
                Teamid = 11,
                Name = "Splash Bros",
                Seed = 2,
                Waiverpriority = 5,
                Lastweekpoints = 123.5,
                Categoryleaguepoints = 7.0,
                Islock = false,
            };

            var dto = team.ToTeamDto();

            Assert.Equal(11, dto.Teamid);
            Assert.Equal("Splash Bros", dto.Name);
            Assert.Equal(2, dto.Seed);
            Assert.Equal(5, dto.Waiverpriority);
            Assert.Equal(123.5, dto.Lastweekpoints);
            Assert.Equal(7.0, dto.Categoryleaguepoints);
            Assert.False(dto.Islock);
            Assert.Null(dto.Competesinleague);
        }

        [Fact]
        public void ToPlayerDto_copies_all_scalar_fields_and_maps_the_position_code()
        {
            var player = new Player
            {
                Playerid = 21,
                Name = "Stephen",
                Surname = "Curry",
                Irlteamname = "Golden State Warriors",
                Playerposition = (int)PlayerPositionEnum.G,
                Points = 26.4m,
                Rebounds = 4.5m,
                Assists = 6.3m,
                Steals = 1.1m,
                Blocks = 0.4m,
                Threepointers = 4.8m,
                Turnovers = 3.1m,
                Fieldgoal = 0.453m,
                Freethrow = 0.915m,
            };

            var dto = player.ToPlayerDto();

            Assert.Equal(21, dto.Playerid);
            Assert.Equal("Stephen", dto.Name);
            Assert.Equal("Curry", dto.Surname);
            Assert.Equal("Golden State Warriors", dto.Irlteamname);
            Assert.Equal("G", dto.Position);
            Assert.Equal(26.4m, dto.Points);
            Assert.Equal(4.5m, dto.Rebounds);
            Assert.Equal(6.3m, dto.Assists);
            Assert.Equal(1.1m, dto.Steals);
            Assert.Equal(0.4m, dto.Blocks);
            Assert.Equal(4.8m, dto.Threepointers);
            Assert.Equal(3.1m, dto.Turnovers);
            Assert.Equal(0.453m, dto.Fieldgoal);
            Assert.Equal(0.915m, dto.Freethrow);
        }

        [Theory]
        [InlineData((int)PlayerPositionEnum.G, "G")]
        [InlineData((int)PlayerPositionEnum.F, "F")]
        [InlineData((int)PlayerPositionEnum.C, "C")]
        [InlineData((int)PlayerPositionEnum.GF, "GF")]
        [InlineData((int)PlayerPositionEnum.CF, "CF")]
        [InlineData((int)PlayerPositionEnum.FG, "FG")]
        [InlineData((int)PlayerPositionEnum.UNKOWN, "UNKOWN")]
        [InlineData(null, "UNKOWN")]
        public void ToPlayerDto_maps_every_position_code_to_its_label(int? code, string expected)
        {
            var dto = new Player { Name = "A", Surname = "B", Playerposition = code }.ToPlayerDto();

            Assert.Equal(expected, dto.Position);
        }

        [Fact]
        public void ToUserTeamDto_copies_team_fields_the_league_label_and_the_roster()
        {
            var team = new Team
            {
                Teamid = 11,
                Name = "Splash Bros",
                Seed = 2,
                Waiverpriority = 5,
                Lastweekpoints = 123.5,
                Categoryleaguepoints = 7.0,
                Islock = false,
                Leagueid = 7,
                League = new League { Leagueid = 7, Name = "Dunk Dynasty" },
            };

            var players = new List<Player>
            {
                new() { Playerid = 21, Name = "Stephen", Surname = "Curry", Playerposition = (int)PlayerPositionEnum.G },
                new() { Playerid = 22, Name = "Draymond", Surname = "Green", Playerposition = (int)PlayerPositionEnum.F },
            };

            var dto = team.ToUserTeamDto(players);

            Assert.Equal(11, dto.Teamid);
            Assert.Equal("Splash Bros", dto.Name);
            Assert.Equal(2, dto.Seed);
            Assert.Equal(5, dto.Waiverpriority);
            Assert.Equal(123.5, dto.Lastweekpoints);
            Assert.Equal(7.0, dto.Categoryleaguepoints);
            Assert.False(dto.Islock);
            Assert.Equal(7, dto.Leagueid);
            Assert.Equal("Dunk Dynasty", dto.Leaguename);
            Assert.Equal([21, 22], dto.Players.Select(p => p.Playerid));
        }

        [Fact]
        public void ToUserTeamDto_yields_an_empty_roster_and_no_league_label_when_there_is_nothing_to_map()
        {
            var dto = new Team { Teamid = 12, Name = "Empty" }.ToUserTeamDto([]);

            Assert.Null(dto.Leagueid);
            Assert.Null(dto.Leaguename);
            Assert.Empty(dto.Players);
        }
    }
}
