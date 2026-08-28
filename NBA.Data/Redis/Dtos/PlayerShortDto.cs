using NBA.Data.Enumerations;
using NBA.Data.Redis.Entities;

namespace NBA.Data.Redis.Dtos
{
    // The player shape that goes into DraftState — the payload clients receive over /draftHub, and the
    // JSON written to draft:state and mirrored into nba.draftsnapshot. It differs from PlayerShort in
    // exactly one way: Position is the readable label ("G", "GF", ...) rather than the PlayerPositionEnum
    // code, because no client should have to know the enum.
    //
    // It lives in NBA.Data rather than NBA.Api/DTOs (rule 5's usual home) because DraftState itself
    // lives here and NBA.Data cannot reference NBA.Api. Nothing else in the API returns it directly.
    public class PlayerShortDto
    {
        public long? PlayerId { get; set; } = null;
        public string? FullName { get; set; } = null;
        public string? Position { get; set; } = null;
    }

    public static class PlayerShortMappings
    {
        // PlayerShort keeps the int code so the roster rules can compare it to Player.Playerposition
        // without a string round trip; the conversion happens here, on the way to a client, using the
        // same map the HTTP player DTOs use.
        public static PlayerShortDto ToPlayerShortDto(this PlayerShort e) => new()
        {
            PlayerId = e.PlayerId,
            FullName = e.FullName,
            Position = e.Position.ToPositionLabel(),
        };

        public static List<PlayerShortDto> ToPlayerShortDtos(this IEnumerable<PlayerShort> players) =>
            players.Select(p => p.ToPlayerShortDto()).ToList();
    }
}
