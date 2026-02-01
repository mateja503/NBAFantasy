using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Player
{
    public long Playerid { get; set; }

    public string Name { get; set; } = null!;

    public long? Teamid { get; set; }

    public long? Leagueid { get; set; }

    public DateTime Tscreated { get; set; }

    public DateTime? Tsupdated { get; set; }

    public string Usercreated { get; set; } = null!;

    public string? Userupdated { get; set; }

    public virtual League? League { get; set; }

    public virtual Team? Team { get; set; }
}
