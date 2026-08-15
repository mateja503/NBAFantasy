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
        public void ToGameRedis_flattens_both_sides_and_normalises_the_date()
        {
            var input = new List<GameInfoResponse>
            {
                new()
                {
                    id = 1038184,
                    // A full ISO timestamp instead of a plain date has been seen in the wild; the
                    // day part is what the schedule buckets are cut on, so it must survive.
                    date = "2026-08-05T00:00:00.000Z",
                    status = "7:30 pm ET",
                    datetime = new DateTime(2026, 8, 5, 23, 30, 0, DateTimeKind.Utc),
                    time = "23:30",
                    postseason = true,
                    postponed = false,
                    home_team_score = 112,
                    visitor_team_score = 109,
                    home_team = new Team { id = 14, full_name = "Los Angeles Lakers", abbreviation = "LAL", city = "Los Angeles" },
                    visitor_team = new Team { id = 2, full_name = "Boston Celtics", abbreviation = "BOS", city = "Boston" },
                }
            };

            var game = Assert.Single(Adapter.ToGameRedis(input));

            Assert.Equal(1038184, game.GameId);
            Assert.Equal("2026-08-05", game.Date);
            Assert.Equal("7:30 pm ET", game.Status);
            Assert.True(game.Postseason);
            Assert.Equal("LAL", game.HomeTeam!.Abbreviation);
            Assert.Equal("Los Angeles", game.HomeTeam.City);
            Assert.Equal(112, game.HomeTeam.Score);
            Assert.Equal("BOS", game.VisitorTeam!.Abbreviation);
            Assert.Equal(109, game.VisitorTeam.Score);
        }

        [Fact]
        public void ToGameRedis_nulls_a_missing_datetime_rather_than_emitting_year_one()
        {
            var input = new List<GameInfoResponse>
            {
                new()
                {
                    id = 1,
                    date = "2026-08-05",
                    status = "7:30 pm ET",
                    time = "23:30",
                    postseason = false,
                    postponed = false,
                    home_team = new Team { id = 14, full_name = "Los Angeles Lakers" },
                    visitor_team = new Team { id = 2, full_name = "Boston Celtics" },
                }
            };

            var game = Assert.Single(Adapter.ToGameRedis(input));

            Assert.Null(game.StartTime);
            Assert.Equal(0, game.HomeTeam!.Score);
        }

        [Fact]
        public void ToPlayerRedisFromDB_carries_the_position_code_across()
        {
            var dbPlayers = Adapter.ToPlayerDb(new List<PlayerInfoResponse>
            {
                new() { id = 9, first_name = "Jrue", last_name = "Holiday", position = "G-F" }
            });

            var redis = Adapter.ToPlayerRedisFromDB(dbPlayers);

            var entry = Assert.Single(redis);
            Assert.Equal(9, entry.PlayerId);
            Assert.Equal("Jrue Holiday", entry.FullName);
            Assert.Equal((int)PlayerPositionEnum.GF, entry.Position);
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
            Assert.Equal((int)PlayerPositionEnum.G, entry.Position);
        }
    }
}
