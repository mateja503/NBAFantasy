

using ExternalClients.Poco;

namespace ExternalClients.Response
{
    // Shared by every /v1/games call (single date or start_date/end_date range) — the payload shape
    // is identical, only the query differs.
    public record GetGamesResponse
    {
        public required List<GameInfoResponse> data { get; init; }
        public required MetaData meta { get; init; }
    }

    public record GameInfoResponse
    {
        public required long id { get; init; }
        public required string date { get; init; }
        public required string status { get; init; }
        public DateTime datetime { get; init; }
        public required string time { get; init;  }
        public required bool postseason { get; init; }
        public required bool postponed { get; init; }
        public required Team home_team { get; init; }
        public required Team visitor_team { get; init; }

        // Zero until a game has actually been played. Deliberately not `required`: a scheduled-game
        // payload that omits them must still deserialize.
        public int home_team_score { get; init; }
        public int visitor_team_score { get; init; }
    }

    public record Team
    {
        public required long id { get; init; }
        public required string full_name { get; init; }

        // Also not `required` — balldontlie always sends them today, but keeping them optional means
        // an older stubbed/recorded body in NBA.Tests does not start failing deserialization.
        public string abbreviation { get; init; } = string.Empty;
        public string city { get; init; } = string.Empty;
    }
}
