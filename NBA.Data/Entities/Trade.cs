using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Trade
{
    public Guid Tradeid { get; set; }

    public long Leagueid { get; set; }

    public long Fromteamid { get; set; }

    public long Toteamid { get; set; }

    public List<long> Playerids { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime Tscreated { get; set; }

    public DateTime Tsexpires { get; set; }

    public virtual Team Fromteam { get; set; } = null!;

    public virtual League League { get; set; } = null!;

    public virtual Team Toteam { get; set; } = null!;
}
