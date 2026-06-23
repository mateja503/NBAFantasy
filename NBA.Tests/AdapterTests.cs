using ExternalClients.Response;
using NBA.Data.Enumerations;
using NBA.Service;
using Xunit;

namespace NBA.Tests
{
    // Pure mapping logic in NBA.Service.Adapter is the cheapest high-value thing to lock down:
    // the position string -> enum -> string round trip is easy to break and has no DB dependency.
    public class AdapterTests
    {
        [Theory]
        [InlineData("G", (int)PlayerPositionEnum.G)]
        [InlineData("g", (int)PlayerPositionEnum.G)]
        [InlineData("F", (int)PlayerPositionEnum.F)]
        [InlineData("C", (int)PlayerPositionEnum.C)]
        [InlineData("G-F", (int)PlayerPositionEnum.GF)]
        [InlineData("C-F", (int)PlayerPositionEnum.CF)]
        [InlineData("F-G", (int)PlayerPositionEnum.FG)]
        [InlineData("PG", (int)PlayerPositionEnum.UNKOWN)]
        [InlineData("", (int)PlayerPositionEnum.UNKOWN)]
        public void ToPlayerDb_maps_position_string_to_enum(string position, int expected)
        {
            var input = new List<PlayerInfoResponse>
            {
                new() { id = 1, first_name = "Test", last_name = "Player", position = position }
            };

            var result = Adapter.ToPlayerDb(input);

            Assert.Single(result);
            Assert.Equal(expected, result[0].Playerposition);
        }

        [Fact]
        public void ToPlayerDb_copies_identity_and_team_fields()
        {
            var input = new List<PlayerInfoResponse>
            {
                new()
                {
                    id = 237,
                    first_name = "LeBron",
                    last_name = "James",
                    position = "F",
                    team = new TeamInfoResponse
                    {
                        id = 14,
                        conference = "West",
                        division = "Pacific",
                        city = "Los Angeles",
                        name = "Lakers",
                        full_name = "Los Angeles Lakers",
                        abbreviation = "LAL"
                    }
                }
            };

            var player = Assert.Single(Adapter.ToPlayerDb(input));

            Assert.Equal(237, player.Playerid);
            Assert.Equal("LeBron", player.Name);
            Assert.Equal("James", player.Surname);
            Assert.Equal("Los Angeles Lakers", player.Irlteamname);
            Assert.Equal(14, player.Irlteamid);
        }

        [Fact]
        public void ToPlayerDb_handles_null_team()
        {
            var input = new List<PlayerInfoResponse>
            {
                new() { id = 5, first_name = "No", last_name = "Team", position = "C", team = null }
            };

            var player = Assert.Single(Adapter.ToPlayerDb(input));

            Assert.Null(player.Irlteamname);
            Assert.Null(player.Irlteamid);
        }

        [Fact]
        public void ToPlayerRedisFromDB_round_trips_position_back_to_string()
        {
            var dbPlayers = Adapter.ToPlayerDb(new List<PlayerInfoResponse>
            {
                new() { id = 9, first_name = "Jrue", last_name = "Holiday", position = "G-F" }
            });

            var redis = Adapter.ToPlayerRedisFromDB(dbPlayers);

            var entry = Assert.Single(redis);
            Assert.Equal(9, entry.PlayerId);
            Assert.Equal("Jrue Holiday", entry.FullName);
            Assert.Equal("GF", entry.Position);
        }

        [Fact]
        public void ToPlayerRedis_builds_full_name_from_response()
        {
            var redis = Adapter.ToPlayerRedis(new List<PlayerInfoResponse>
            {
                new() { id = 3, first_name = "Stephen", last_name = "Curry", position = "G" }
            });

            var entry = Assert.Single(redis);
            Assert.Equal("Stephen Curry", entry.FullName);
            Assert.Equal("G", entry.Position);
        }
    }
}
