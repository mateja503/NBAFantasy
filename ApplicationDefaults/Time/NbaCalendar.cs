using System.Globalization;

namespace ApplicationDefaults.Time
{
    /// <summary>
    /// balldontlie schedules are US dates, so "today" has to be resolved in the NBA's timezone and
    /// not in UTC (a 20:00 ET tip-off is already the next day in UTC). Every date boundary the
    /// schedule feature needs is derived here so the HTTP client and the service can never disagree
    /// about which day it is.
    /// </summary>
    public static class NbaCalendar
    {
        public static readonly TimeZoneInfo NbaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");

        public static DateOnly Today() =>
            DateOnly.FromDateTime(TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, NbaTimeZone));

        /// <summary>
        /// The Sunday that closes the calendar week <paramref name="day"/> falls in (weeks run
        /// Monday -> Sunday). Returns <paramref name="day"/> itself when it is already a Sunday.
        /// </summary>
        public static DateOnly EndOfWeek(DateOnly day)
        {
            // DayOfWeek.Sunday is 0, so Sunday has to be treated as day 7 of the week it closes
            // rather than as the first day of the next one.
            var dayIndex = day.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)day.DayOfWeek;
            return day.AddDays(7 - dayIndex);
        }

        /// <summary>The yyyy-MM-dd form balldontlie expects in query strings and returns in payloads.</summary>
        public static string ToApiDate(this DateOnly date) =>
            date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

        /// <summary>
        /// Normalises a date coming back from balldontlie to yyyy-MM-dd. The field is documented as a
        /// plain date but has been observed carrying a full ISO timestamp, so the day part is taken
        /// rather than trusted wholesale. Null/short values collapse to an empty string, which simply
        /// matches no bucket instead of throwing.
        /// </summary>
        public static string ToApiDatePart(string? apiDate) =>
            apiDate is not null && apiDate.Length >= 10 ? apiDate[..10] : string.Empty;
    }
}
