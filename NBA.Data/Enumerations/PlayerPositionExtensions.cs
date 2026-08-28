namespace NBA.Data.Enumerations
{
    // The single int -> label conversion. Positions are stored as PlayerPositionEnum codes in both
    // Postgres (Player.Playerposition) and Redis (PlayerShort.Position) so the roster rules can compare
    // them without a string round trip, but no client should have to know the enum. This lives next to
    // the enum rather than in NBA.Api/Mappings because both boundaries need it: the HTTP player DTOs
    // and PlayerShort.PositionLabel, which rides along on the draft-hub payload.
    public static class PlayerPositionExtensions
    {
        // Unrecognised and missing codes both read as UNKOWN — a player with a position we never mapped
        // is still a player, and dropping them from a draft board would be worse than labelling them.
        public static string ToPositionLabel(this int? playerPosition) => playerPosition switch
        {
            (int)PlayerPositionEnum.G => "G",
            (int)PlayerPositionEnum.F => "F",
            (int)PlayerPositionEnum.C => "C",
            (int)PlayerPositionEnum.GF => "GF",
            (int)PlayerPositionEnum.CF => "CF",
            (int)PlayerPositionEnum.FG => "FG",
            _ => nameof(PlayerPositionEnum.UNKOWN),
        };

        // Convenience for the non-nullable callers (Adapter.ToPositionCode, the Redis converter) so they
        // do not each cast to int? at the call site.
        public static string ToPositionLabel(this int playerPosition) => ToPositionLabel((int?)playerPosition);
    }
}
