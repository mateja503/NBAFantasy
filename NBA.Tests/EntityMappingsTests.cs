using NBA.Api.Mappings;
using NBA.Data.Entities;
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
    }
}
