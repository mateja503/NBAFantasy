using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Leagueplayer
{
    public long Leagueplayerid { get; set; }

    public long Playerid { get; set; }

    public long Leagueid { get; set; }

    public virtual League League { get; set; } = null!;

    public virtual Player Player { get; set; } = null!;
}
