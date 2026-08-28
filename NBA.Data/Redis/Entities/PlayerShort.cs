using System;
using System.Collections.Generic;
using System.Text;

namespace NBA.Data.Redis.Entities
{
    public class PlayerShort
    {
        public long? PlayerId { get; set; } = null;
        public string? FullName { get; set; } = null;

        // PlayerPositionEnum as an int, matching Player.Playerposition in Postgres. Stored as the code
        // rather than a label so roster rules can compare positions without a string round trip. Clients
        // never see this shape: what goes into DraftState is PlayerShortDto, which carries the label.
        public int? Position { get; set; } = null;
    }
}
