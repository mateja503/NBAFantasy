using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Teamplayer
{
    public long Teamplayerid { get; set; }

    public long Playerid { get; set; }

    public long Teamid { get; set; }

    public virtual Player Player { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
