using ApplicationDefaults.Time;
using NBA.Data.Redis.Entities;
using NBA.Service.Game;
using System.Globalization;
using Xunit;

namespace NBA.Tests
{
    // The schedule endpoint's only real logic is where the day boundaries fall, so that is what is
    // pinned here. Both pieces are pure, so no Redis, HttpClient or Aspire stack is involved.
    public class GameScheduleTests
    {
        private static GameShort Game(long id, string date) => new()
        {
            GameId = id,
            Date = date,
            HomeTeam = new GameTeamShort { TeamId = 14, FullName = "Los Angeles Lakers" },
            VisitorTeam = new GameTeamShort { TeamId = 2, FullName = "Boston Celtics" },
        };

        #region NbaCalendar

        [Theory]
        // Monday 2026-08-03 .. Sunday 2026-08-09 are one week; every day resolves to the same Sunday.
        [InlineData("2026-08-03", "2026-08-09")] // Monday
        [InlineData("2026-08-06", "2026-08-09")] // Thursday
        [InlineData("2026-08-08", "2026-08-09")] // Saturday
        [InlineData("2026-08-09", "2026-08-09")] // Sunday closes its own week
        [InlineData("2026-08-10", "2026-08-16")] // the next Monday starts a new one
        public void EndOfWeek_returns_the_sunday_that_closes_the_week(string day, string expected)
        {
            var endOfWeek = NbaCalendar.EndOfWeek(DateOnly.Parse(day, CultureInfo.InvariantCulture));

            Assert.Equal(expected, endOfWeek.ToApiDate());
        }

        [Theory]
        [InlineData("2026-08-05T20:00:00Z", "2026-08-05")]
        [InlineData("2026-08-06", "2026-08-06")]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData("2026-08", "")]
        public void ToApiDatePart_keeps_only_the_day_and_never_throws(string? apiDate, string expected)
        {
            Assert.Equal(expected, NbaCalendar.ToApiDatePart(apiDate));
        }

        #endregion

        #region Bucketing

        [Fact]
        public void BucketByDay_splits_today_tomorrow_and_the_rest_of_the_week_without_overlap()
        {
            var today = new DateOnly(2026, 8, 5);   // Wednesday
            var tomorrow = today.AddDays(1);

            var games = new List<GameShort>
            {
                Game(1, "2026-08-05"),
                Game(2, "2026-08-05"),
                Game(3, "2026-08-06"),
                Game(4, "2026-08-07"),
                Game(5, "2026-08-09"),
            };

            var scheduled = GameService.BucketByDay(games, today, tomorrow);

            Assert.Equal(new long[] { 1, 2 }, scheduled.Today.Select(g => g.GameId).ToArray());
            Assert.Equal(new long[] { 3 }, scheduled.Tomorrow.Select(g => g.GameId).ToArray());
            // The requirement: the week bucket must not repeat today's or tomorrow's games.
            Assert.Equal(new long[] { 4, 5 }, scheduled.RestOfWeek.Select(g => g.GameId).ToArray());
        }

        [Fact]
        public void BucketByDay_leaves_the_week_empty_when_nothing_falls_after_tomorrow()
        {
            var today = new DateOnly(2026, 8, 8);   // Saturday — only Sunday is left in the week
            var tomorrow = today.AddDays(1);

            var scheduled = GameService.BucketByDay(
                [Game(1, "2026-08-08"), Game(2, "2026-08-09")], today, tomorrow);

            Assert.Single(scheduled.Today);
            Assert.Single(scheduled.Tomorrow);
            Assert.Empty(scheduled.RestOfWeek);
        }

        [Fact]
        public void BucketByDay_still_fills_tomorrow_when_it_lands_in_the_next_week()
        {
            var today = new DateOnly(2026, 8, 9);   // Sunday — tomorrow is next Monday
            var tomorrow = today.AddDays(1);

            var scheduled = GameService.BucketByDay(
                [Game(1, "2026-08-09"), Game(2, "2026-08-10")], today, tomorrow);

            Assert.Single(scheduled.Today);
            Assert.Equal(new long[] { 2 }, scheduled.Tomorrow.Select(g => g.GameId).ToArray());
            Assert.Empty(scheduled.RestOfWeek);
        }

        [Fact]
        public void BucketByDay_drops_games_with_an_unusable_date_instead_of_throwing()
        {
            var today = new DateOnly(2026, 8, 5);

            var scheduled = GameService.BucketByDay(
                [Game(1, string.Empty), new GameShort { GameId = 2, Date = null }], today, today.AddDays(1));

            Assert.Empty(scheduled.Today);
            Assert.Empty(scheduled.Tomorrow);
            Assert.Empty(scheduled.RestOfWeek);
        }

        #endregion
    }
}
